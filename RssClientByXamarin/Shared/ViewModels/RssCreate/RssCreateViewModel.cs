using System;
using System.Reactive;
using Droid.EmbeddedResourse;
using JetBrains.Annotations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shared.Extensions;
using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;
using Shared.Services.Rss;

namespace Shared.ViewModels.RssCreate
{
    public class RssCreateViewModel : ViewModel
    {
        public RssCreateViewModel([NotNull] IRssService service, [NotNull] INavigator navigator)
        {
            Url = Strings.CreateRssUrlDefault;

            CreateCommand = ReactiveCommand.CreateFromTask(async token => await service.AddAsync(Url, token)).NotNull();
            CreateCommand.Subscribe(_ => navigator.GoBack());
        }

        [Reactive] [CanBeNull] public string Url { get; set; }

        [NotNull] public ReactiveCommand<Unit, Unit> CreateCommand { get; }
    }
}
