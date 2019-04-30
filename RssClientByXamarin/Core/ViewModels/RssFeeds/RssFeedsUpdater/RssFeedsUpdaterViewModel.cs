using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Core.Infrastructure.ViewModels;
using Core.Services.RssFeeds;
using JetBrains.Annotations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Core.ViewModels.RssFeeds.RssFeedsUpdater
{
    /// <summary>
    /// Single instance
    /// </summary>
    public class RssFeedsUpdaterViewModel : ViewModel
    {
        [NotNull] private readonly IRssFeedService _rssFeedService;
        [NotNull] private readonly ISubject<RssFeedServiceModel> _updatedRss = new Subject<RssFeedServiceModel>();
        private extern bool IsExecuting { [ObservableAsProperty] get; }
        
        public RssFeedsUpdaterViewModel([NotNull] IRssFeedService rssFeedService)
        {
            _rssFeedService = rssFeedService;
            
            UpdateCommand = ReactiveCommand.CreateFromTask(DoUpdate, this.WhenAnyValue(model => model.IsExecuting));
            UpdateCommand.IsExecuting.ToPropertyEx(this, model => model.IsExecuting);
            UpdatedRss = _updatedRss.AsObservable();
        }

        [NotNull] public ReactiveCommand<Unit, Unit> UpdateCommand { get; }
        
        [NotNull] public IObservable<RssFeedServiceModel> UpdatedRss { get; }
        
        private Task DoUpdate(CancellationToken token)
        {
            return Task.Run(async () =>
            {
                var feeds = await _rssFeedService.GetListAsync(token);

                var updatable = feeds.Where(w => w != null).OrderBy(w => w.UpdateTime).ToList();

                foreach (var rssServiceModel in updatable)
                {
                    await _rssFeedService.LoadAndUpdateAsync(rssServiceModel.Id, token);
                    var newItem = await _rssFeedService.GetAsync(rssServiceModel.Id, token);
                    _updatedRss.OnNext(newItem);
                }
            }, token);
        }
    }
}