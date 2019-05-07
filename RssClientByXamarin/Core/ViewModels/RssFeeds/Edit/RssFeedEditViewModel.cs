using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Extensions;
using Core.Infrastructure.Navigation;
using Core.Infrastructure.ViewModels;
using Core.Resources;
using Core.Services.RssFeeds;
using JetBrains.Annotations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Core.ViewModels.RssFeeds.Edit
{
    public class RssFeedEditViewModel : ViewModelWithParameter<RssEditParameters>
    {
        [NotNull] private readonly IRssFeedService _rssFeedService;

        public RssFeedEditViewModel([NotNull] IRssFeedService rssFeedService, [NotNull] RssEditParameters parameters,
            [NotNull] INavigator navigator) :
            base(parameters)
        {
            _rssFeedService = rssFeedService;

            LoadCommand = ReactiveCommand.CreateFromTask<Unit, RssFeedServiceModel>(async (s, token) =>
                    await _rssFeedService.GetAsync(parameters.RssId, token))
                .NotNull();
            LoadCommand.ToPropertyEx(this, x => x.RssFeedServiceModel);

            UpdateCommand = ReactiveCommand.CreateFromTask(UpdateRssUrl, this.WhenAnyValue(w => w.IsUrlInvalid).Select(w => !w)).NotNull();
            UpdateCommand.Subscribe(_ => navigator.GoBack());

            this.WhenAnyValue(w => w.RssFeedServiceModel)
                .NotNull()
                .Subscribe(w => Url = w?.Rss)
                .NotNull();

            this.WhenAnyValue(w => w.IsUrlInvalid, b => b ? Strings.UrlIsNotValid : string.Empty)
                .ToPropertyEx(this, model => model.ErrorMessage);
        }

        [CanBeNull] [Reactive] public string Url { get; set; }

        [NotNull] public extern RssFeedServiceModel RssFeedServiceModel { [ObservableAsProperty] get; }

        [NotNull] public ReactiveCommand<Unit, RssFeedServiceModel> LoadCommand { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> UpdateCommand { get; }
        
        [Reactive]
        public bool IsUrlInvalid { get; set; }

        public extern string ErrorMessage { [ObservableAsProperty] get; }

        [NotNull]
        private async Task UpdateRssUrl(CancellationToken token)
        {
            var model = RssFeedServiceModel;
            model.Rss = Url;
            await _rssFeedService.UpdateAsync(model, token);
        }
    }
}
