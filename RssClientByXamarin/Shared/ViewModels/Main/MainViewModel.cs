using System.Reactive;
using Autofac;
using Droid.Repository.Configuration;
using ReactiveUI;
using Shared.Configuration.Settings;
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
        private readonly IConfigurationRepository _configurationRepository;
        private readonly INavigator _navigator;

        public MainViewModel(IConfigurationRepository configurationRepository, INavigator navigator)
        {
            _configurationRepository = configurationRepository;
            _navigator = navigator;
            
            OpenRootScreenCommand = ReactiveCommand.Create(DoOpenRootScreen);
            OpenFeedlySearchCommand = ReactiveCommand.Create(DoOpenFeedlySearch);
            OpenFavoriteMessagesCommand = ReactiveCommand.Create(DoOpenFavoriteMessages);
            OpenSettingsCommand = ReactiveCommand.Create(DoOpenSettings);
            OpenContactsCommand = ReactiveCommand.Create(DoOpenContacts);
            OpenAboutCommand = ReactiveCommand.Create(DoOpenAbout);
        }

        public ReactiveCommand<Unit, Unit> OpenRootScreenCommand { get; }

        public ReactiveCommand<Unit, Unit> OpenFeedlySearchCommand { get; }
        
        public ReactiveCommand<Unit, Unit> OpenFavoriteMessagesCommand { get; }
        
        public ReactiveCommand<Unit, Unit> OpenSettingsCommand { get; }
        
        public ReactiveCommand<Unit, Unit> OpenContactsCommand { get; }
        
        public ReactiveCommand<Unit, Unit> OpenAboutCommand { get; }
        
        public AppConfiguration AppConfiguration => _configurationRepository.GetSettings<AppConfiguration>();
        
        private void DoOpenRootScreen()
        {
            var appConfiguration = AppConfiguration;

            if (appConfiguration.StartPage == StartPage.RssList)
            {
                _navigator.Go(App.Container.Resolve<IWay<RssListViewModel>>());
            }
            else if (appConfiguration.StartPage == StartPage.AllMessages)
            {
                _navigator.Go(App.Container.Resolve<IWay<RssAllMessagesViewModel>>());
            }
        }
        
        private void DoOpenFeedlySearch()
        {
            _navigator.Go(App.Container.Resolve<IWay<FeedlySearchViewModel>>());
        }
        
        private void DoOpenFavoriteMessages()
        {
            _navigator.Go(App.Container.Resolve<IWay<RssFavoriteMessagesViewModel>>());
        }
        
        private void DoOpenSettings()
        {
            _navigator.Go(App.Container.Resolve<IWay<SettingsViewModel>>());
        }
        
        private void DoOpenContacts()
        {
            _navigator.Go(App.Container.Resolve<IWay<ContactsViewModel>>());
        }
        
        private void DoOpenAbout()
        {
            _navigator.Go(App.Container.Resolve<IWay<AboutViewModel>>());
        }
    }
}