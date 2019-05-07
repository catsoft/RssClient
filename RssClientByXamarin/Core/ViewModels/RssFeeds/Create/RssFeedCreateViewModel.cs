using System;
using System.Reactive;
using System.Reactive.Linq;
using Core.Extensions;
using Core.Infrastructure.Navigation;
using Core.Infrastructure.ViewModels;
using Core.Resources;
using Core.Services.RssFeeds;
using Core.ViewModels.RssFeeds.RssFeedsUpdater;
using JetBrains.Annotations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Core.ViewModels.RssFeeds.Create
{
    public class RssFeedCreateViewModel : ViewModel
    {
        public RssFeedCreateViewModel([NotNull] IRssFeedService feedService,
            [NotNull] INavigator navigator,
            [NotNull] RssFeedsUpdaterViewModel updater)
        {
            Url = Strings.CreateRssUrlDefault;

            CreateCommand = ReactiveCommand
                .CreateFromTask(async token => { await feedService.AddAsync(Url, token); }, this.WhenAnyValue(w => w.IsUrlInvalid).Select(w => !w))
                .NotNull();
            CreateCommand.Subscribe(_ => navigator.GoBack());

            CreateCommand.SelectUnit().InvokeCommand(updater.SoftUpdateCommand);

            this.WhenAnyValue(w => w.IsUrlInvalid, b => b ? Strings.UrlIsNotValid : string.Empty)
                .ToPropertyEx(this, model => model.ErrorMessage);
        }

        [Reactive] [CanBeNull] public string Url { get; set; }

        [NotNull] public ReactiveCommand<Unit, Unit> CreateCommand { get; }

        [Reactive]
        public bool IsUrlInvalid { get; set; }

        public extern string ErrorMessage { [ObservableAsProperty] get; }
    }
}