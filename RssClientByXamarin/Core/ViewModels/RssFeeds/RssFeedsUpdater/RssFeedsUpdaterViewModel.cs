using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Core.Extensions;
using Core.Infrastructure.ViewModels;
using Core.Services.RssFeeds;
using JetBrains.Annotations;
using ReactiveUI;

namespace Core.ViewModels.RssFeeds.RssFeedsUpdater
{
    /// <summary>
    /// Single instance
    /// </summary>
    public class RssFeedsUpdaterViewModel : ViewModel
    {
        [NotNull] private readonly IRssFeedService _rssFeedService;
        [NotNull] private readonly ISubject<RssFeedServiceModel> _updatedRss = new Subject<RssFeedServiceModel>();
        
        public RssFeedsUpdaterViewModel([NotNull] IRssFeedService rssFeedService)
        {
            _rssFeedService = rssFeedService;

            UpdateCommand = ReactiveCommand.CreateFromTask<IEnumerable<RssFeedServiceModel>>(DoUpdate).NotNull();
            UpdatedRss = _updatedRss.AsObservable().NotNull();
            
            HardUpdateCommand = ReactiveCommand.CreateFromTask(DoHardUpdate, UpdateCommand.IsExecuting.Select(w => !w)).NotNull();
            SoftUpdateCommand = ReactiveCommand.CreateFromTask(DoSoftUpdate, UpdateCommand.IsExecuting.Select(w => !w)).NotNull();
        }

        [NotNull] public ReactiveCommand<IEnumerable<RssFeedServiceModel>, Unit> UpdateCommand { get; }
        
        [NotNull] public ReactiveCommand<Unit, Unit> HardUpdateCommand { get; }
        
        [NotNull] public ReactiveCommand<Unit, Unit> SoftUpdateCommand { get; }
        
        [NotNull] public IObservable<RssFeedServiceModel> UpdatedRss { get; }

        private Task DoUpdate([NotNull] [ItemNotNull] IEnumerable<RssFeedServiceModel> models, CancellationToken token)
        {
            return Task.Run(async () =>
                {
                    foreach (var rssServiceModel in models)
                    {
                        await _rssFeedService.LoadAndUpdateAsync(rssServiceModel.Id, token);
                        var newItem = await _rssFeedService.GetAsync(rssServiceModel.Id, token);
                        _updatedRss.OnNext(newItem);
                    }
                },
                token);
        }

        private Task DoHardUpdate(CancellationToken token)
        {
            return Task.Run(async () =>
                {
                    var feeds = await _rssFeedService.GetListAsync(token);

                    UpdateCommand.ExecuteIfCan(feeds.OrderByDescending(w => w.CreationTime).ToList());
                },
                token);
        }

        private Task DoSoftUpdate(CancellationToken token)
        {
            return Task.Run(async () =>
                {
                    var feeds = await _rssFeedService.GetListAsync(token);
                    feeds = feeds.Where(w => !w.UpdateTime.HasValue || w.UpdateTime.Value.AddMinutes(5) < DateTimeOffset.Now)
                        .OrderBy(w => w.UpdateTime);
                    UpdateCommand.ExecuteIfCan(feeds.ToList());
                },
                token);
        }
    }
}