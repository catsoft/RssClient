using System.Reactive;
using Autofac;
using Core.Configuration.Settings;
using Core.Extensions;
using Core.Infrastructure.Navigation;
using Core.Infrastructure.ViewModels;
using Core.Repositories.Configurations;
using Core.ViewModels.About;
using Core.ViewModels.Contacts;
using Core.ViewModels.FeedlySearch;
using Core.ViewModels.Messages.AllMessages;
using Core.ViewModels.Messages.FavoriteMessages;
using Core.ViewModels.RssFeeds.List;
using Core.ViewModels.Settings;
using JetBrains.Annotations;
using ReactiveUI;

namespace Core.ViewModels.Main
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
                _navigator.Go(App.Container.Resolve<IWay<RssFeedListViewModel>>().NotNull());
            else if (appConfiguration.StartPage == StartPage.AllMessages)
                _navigator.Go(App.Container.Resolve<IWay<AllMessagesViewModel>>().NotNull());
        }

        private void DoOpenFeedlySearch() { _navigator.Go(App.Container.Resolve<IWay<FeedlySearchViewModel>>().NotNull()); }

        private void DoOpenFavoriteMessages() { _navigator.Go(App.Container.Resolve<IWay<FavoriteMessagesViewModel>>().NotNull()); }

        private void DoOpenSettings() { _navigator.Go(App.Container.Resolve<IWay<SettingsViewModel>>().NotNull()); }

        private void DoOpenContacts() { _navigator.Go(App.Container.Resolve<IWay<ContactsViewModel>>().NotNull()); }

        private void DoOpenAbout() { _navigator.Go(App.Container.Resolve<IWay<AboutViewModel>>().NotNull()); }
    }
}
