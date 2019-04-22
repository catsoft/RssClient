using System;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shared.Extensions;
using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;
using Shared.Services.Rss;

namespace Shared.ViewModels.RssEdit
{
    public class RssEditViewModel : ViewModelWithParameter<RssEditParameters>
    {
        [NotNull] private readonly IRssService _rssService;

        public RssEditViewModel([NotNull] IRssService rssService, [NotNull] RssEditParameters parameters, [NotNull] INavigator navigator) :
            base(parameters)
        {
            _rssService = rssService;

            LoadCommand = ReactiveCommand.CreateFromTask<Unit, RssServiceModel>(async (s, token) =>
                    await _rssService.GetAsync(parameters.RssId, token))
                .NotNull();
            LoadCommand.ToPropertyEx(this, x => x.RssServiceModel);

            UpdateCommand = ReactiveCommand.CreateFromTask(UpdateRssUrl).NotNull();
            UpdateCommand.Subscribe(_ => navigator.GoBack());

            this.WhenAnyValue(w => w.RssServiceModel)
                .NotNull()
                .Subscribe(w => Url = w?.Rss)
                .NotNull();
        }

        [CanBeNull] [Reactive] public string Url { get; set; }

        [NotNull] public extern RssServiceModel RssServiceModel { [ObservableAsProperty] get; }

        [NotNull] public ReactiveCommand<Unit, RssServiceModel> LoadCommand { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> UpdateCommand { get; }

        [NotNull]
        private async Task UpdateRssUrl(CancellationToken token)
        {
            var model = RssServiceModel;
            model.Rss = Url;
            await _rssService.UpdateAsync(model, token);
        }
    }
}
