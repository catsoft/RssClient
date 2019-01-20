using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Autofac;
using Droid.Infrastructure;
using Droid.Screens.Contacts;
using Droid.Screens.Navigation;
using Droid.Screens.RssAllMessagesList;
using Droid.Screens.RssList;
using Droid.Screens.Settings;
using Java.Lang.Annotation;
using Shared;
using Shared.Services.Navigator;
using Shared.ViewModels;
using Fragment = Android.Support.V4.App.Fragment;

namespace Droid.Screens.Main
{
    [Activity(Label = "@string/all_appName", Theme = "@style/AppTheme.NoActionBar")]
    public class MainActivity : FragmentActivity
    {
        private readonly int _mainId = Resource.Id.menuItem_navigationMenu_main;
        private readonly int _settingsId = Resource.Id.menuItem_navigationMenu_settings;
        private readonly int _contactsId = Resource.Id.menuItem_navigationMenu_contacts;
        private readonly int _aboutId = Resource.Id.menuItem_navigationMenu_about;

        private INavigator _navigator;

        protected override int? ContainerId => Resource.Id.frameLayout_rssList_fragmentContainer;
        protected override int ResourceView => Resource.Layout.activity_rss_list;
        public static MainActivity Instance { get; private set; }

        public static void CreateActivity(Activity activity)
        {
            var intent = new Intent(activity, typeof(MainActivity));
            activity.StartActivity(intent);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Instance = this;

            _navigator = App.Container.Resolve<INavigator>();
            
            base.OnCreate(savedInstanceState);

            Title = GetText(Resource.String.rssList_title);

            _navigator.Go(App.Container.Resolve<IWay<RssListViewModel, RssListViewModel.Way.WayData>>());
            
            NavigationView.SetCheckedItem(_mainId);
        }

        public override bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            if (menuItem.ItemId == _mainId)
            {
                _navigator.Go(App.Container.Resolve<IWay<RssListViewModel, RssListViewModel.Way.WayData>>());
            } else if (menuItem.ItemId == _settingsId)
            {
                _navigator.Go(App.Container.Resolve<IWay<SettingsViewModel, SettingsViewModel.Way.WayData>>());
            }
            else if (menuItem.ItemId == _contactsId)
            {
                _navigator.Go(App.Container.Resolve<IWay<ContactsViewModel, ContactsViewModel.Way.WayData>>());
            }
            else if (menuItem.ItemId == _aboutId)
            {
                _navigator.Go(App.Container.Resolve<IWay<AboutViewModel, AboutViewModel.Way.WayData>>());
            }

            menuItem.SetChecked(true);

            DrawerLayout.CloseDrawer(DrawerGravity);

            return true;
        }
    }
}