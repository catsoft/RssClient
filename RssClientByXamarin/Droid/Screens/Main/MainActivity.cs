using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Autofac;
using Droid.Container;
using Droid.Repository;
using Droid.Screens.Navigation;
using Shared;
using Shared.Configuration;
using Shared.Services.Navigator;
using Shared.ViewModels;

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
                    _navigator.Go(App.Container.Resolve<RssListViewModel.Way>());
                else if (appConfiguration.StartPage == StartPage.AllMessages)
                    _navigator.Go(App.Container.Resolve<RssAllMessagesViewModel.Way>());

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
                    _navigator.Go(App.Container.Resolve<RssListViewModel.Way>());
                else if (appConfiguration.StartPage == StartPage.AllMessages)
                    _navigator.Go(App.Container.Resolve<RssAllMessagesViewModel.Way>());
            }
            else if (menuItem.ItemId == Resource.Id.menuItem_navigationMenu_recommended)
            {
                _navigator.Go(App.Container.Resolve<RecommendedCategoryListViewModel.Way>());
            }
            else if (menuItem.ItemId == Resource.Id.menuItem_navigationMenu_settings)
            {
                _navigator.Go(App.Container.Resolve<SettingsViewModel.Way>());
            }
            else if (menuItem.ItemId == Resource.Id.menuItem_navigationMenu_contacts)
            {
                _navigator.Go(App.Container.Resolve<ContactsViewModel.Way>());
            }
            else if (menuItem.ItemId == Resource.Id.menuItem_navigationMenu_about)
            {
                _navigator.Go(App.Container.Resolve<AboutViewModel.Way>());
            }

            menuItem.SetChecked(true);

            DrawerLayout.CloseDrawer(DrawerGravity);

            return true;
        }
    }
}