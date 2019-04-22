using System.Reactive;
using Autofac;
using Droid.Repositories.Configuration;
using JetBrains.Annotations;
using ReactiveUI;
using Shared.Configuration.Settings;
using Shared.Extensions;
using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;
using Shared.ViewModels.About;
using Shared.ViewModels.Contacts;
using Shared.ViewModels.FeedlySearch;
using Shared.ViewModels.RssAllMessages;
using Shared.ViewModels.RssFavoriteMessages;
using Shared.ViewModels.RssList;
using Shared.ViewModels.Settings;

namespace Shared.ViewModels.Main
{
    public class MainViewModel : ViewModel
    {
        [NotNull] private readonly IConfigurationRepository _configurationRepository;
        [NotNull] private readonly INavigator _navigator;

        public MainViewModel([NotNull] IConfigurationRepository configurationRepository, [NotNull] INavigator navigator)
        {
            _configurationRepository = configurationRepository;
            _navigator = navigator;

            OpenRootScreenCommand = ReactiveCommand.Create(DoOpenRootScreen).NotNull();
            OpenFeedlySearchCommand = ReactiveCommand.Create(DoOpenFeedlySearch).NotNull();
            OpenFavoriteMessagesCommand = ReactiveCommand.Create(DoOpenFavoriteMessages).NotNull();
            OpenSettingsCommand = ReactiveCommand.Create(DoOpenSettings).NotNull();
            OpenContactsCommand = ReactiveCommand.Create(DoOpenContacts).NotNull();
            OpenAboutCommand = ReactiveCommand.Create(DoOpenAbout).NotNull();
        }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenRootScreenCommand { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenFeedlySearchCommand { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenFavoriteMessagesCommand { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenSettingsCommand { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenContactsCommand { get; }

        [NotNull] public ReactiveCommand<Unit, Unit> OpenAboutCommand { get; }

        [NotNull] public AppConfiguration AppConfiguration => _configurationRepository.GetSettings<AppConfiguration>();

        private void DoOpenRootScreen()
        {
            var appConfiguration = AppConfiguration;

            if (appConfiguration.StartPage == StartPage.RssList)
                _navigator.Go(App.Container.Resolve<IWay<RssListViewModel>>().NotNull());
            else if (appConfiguration.StartPage == StartPage.AllMessages)
                _navigator.Go(App.Container.Resolve<IWay<RssAllMessagesViewModel>>().NotNull());
        }

        private void DoOpenFeedlySearch() { _navigator.Go(App.Container.Resolve<IWay<FeedlySearchViewModel>>().NotNull()); }

        private void DoOpenFavoriteMessages() { _navigator.Go(App.Container.Resolve<IWay<RssFavoriteMessagesViewModel>>().NotNull()); }

        private void DoOpenSettings() { _navigator.Go(App.Container.Resolve<IWay<SettingsViewModel>>().NotNull()); }

        private void DoOpenContacts() { _navigator.Go(App.Container.Resolve<IWay<ContactsViewModel>>().NotNull()); }

        private void DoOpenAbout() { _navigator.Go(App.Container.Resolve<IWay<AboutViewModel>>().NotNull()); }
    }
}
