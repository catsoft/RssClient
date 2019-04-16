using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Autofac;
using Droid.Container;
using Droid.NativeExtension;
using Droid.Repository.Configuration;
using Droid.Screens.Navigation;
using Shared;
using Shared.Configuration.Settings;
using Shared.Infrastructure.Navigation;
using Shared.ViewModels.About;
using Shared.ViewModels.Contacts;
using Shared.ViewModels.FeedlySearch;
using Shared.ViewModels.RssAllMessages;
using Shared.ViewModels.RssFavoriteMessages;
using Shared.ViewModels.RssList;
using Shared.ViewModels.Settings;

namespace Droid.Screens.Main
{
    [Activity(Label = "@string/all_appName", Theme = "@style/AppTheme.Default.NoActionBar")]
    public class MainActivity : FragmentActivity
    {
        [Inject] private INavigator _navigator;

        [Inject] private IConfigurationRepository _configurationRepository;

        protected override int? ContainerId => Resource.Id.frameLayout_rssList_fragmentContainer;
        protected override int ResourceView => Resource.Layout.activity_main;
        public static MainActivity Instance { get; private set; }

        public static void CreateActivity(Activity activity)
        {
            var intent = new Intent(activity, typeof(MainActivity));
            activity.StartActivity(intent);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Instance = this;

            base.OnCreate(savedInstanceState);

            Title = GetText(Resource.String.rssList_title);

            if (savedInstanceState == null)
            {
                // TODO Вынести в отдельный роут
                var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();

                if (appConfiguration.StartPage == StartPage.RssList)
                    _navigator.Go(App.Container.Resolve<IWay<RssListViewModel>>());
                else if (appConfiguration.StartPage == StartPage.AllMessages)
                    _navigator.Go(App.Container.Resolve<IWay<RssAllMessagesViewModel>>());

                NavigationView.SetCheckedItem(Resource.Id.menuItem_navigationMenu_main);
            }
        }

        public override bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            if (menuItem.ItemId == Resource.Id.menuItem_navigationMenu_main)
            {
                // TODO Вынести в отдельный роут
                var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();

                if (appConfiguration.StartPage == StartPage.RssList)
                    _navigator.Go(App.Container.Resolve<IWay<RssListViewModel>>());
                else if (appConfiguration.StartPage == StartPage.AllMessages)
                    _navigator.Go(App.Container.Resolve<IWay<RssAllMessagesViewModel>>());
            }
            else if (menuItem.ItemId == Resource.Id.menuItem_navigationMenu_feedlySearch)
            {
                _navigator.Go(App.Container.Resolve<IWay<FeedlySearchViewModel>>());
            }
            else if (menuItem.ItemId == Resource.Id.menuItem_navigationMenu_favorite)
            {
                _navigator.Go(App.Container.Resolve<IWay<RssFavoriteMessagesViewModel>>());
            }
            else if (menuItem.ItemId == Resource.Id.menuItem_navigationMenu_settings)
            {
                _navigator.Go(App.Container.Resolve<IWay<SettingsViewModel>>());
            }
            else if (menuItem.ItemId == Resource.Id.menuItem_navigationMenu_contacts)
            {
                _navigator.Go(App.Container.Resolve<IWay<ContactsViewModel>>());
            }
            else if (menuItem.ItemId == Resource.Id.menuItem_navigationMenu_rate)
            {
                this.RateInMarket();
            }
            else if (menuItem.ItemId == Resource.Id.menuItem_navigationMenu_about)
            {
                _navigator.Go(App.Container.Resolve<IWay<AboutViewModel>>());
            }

            menuItem.SetChecked(true);

            DrawerLayout.CloseDrawer(DrawerGravity);

            return true;
        }
    }
}