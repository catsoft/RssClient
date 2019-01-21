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
            
            NavigationView.SetCheckedItem(Resource.Id.menuItem_navigationMenu_main);
        }

        public override bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            if (menuItem.ItemId == Resource.Id.menuItem_navigationMenu_main)
            {
                _navigator.Go(App.Container.Resolve<IWay<RssListViewModel, RssListViewModel.Way.WayData>>());
            } 
            else if (menuItem.ItemId == Resource.Id.menuItem_navigationMenu_recommended)
            {
                _navigator.Go(App.Container.Resolve<IWay<RecommendationViewModel, RecommendationViewModel.Way.WayData>>());
            }
            else if (menuItem.ItemId == Resource.Id.menuItem_navigationMenu_settings)
            {
                _navigator.Go(App.Container.Resolve<IWay<SettingsViewModel, SettingsViewModel.Way.WayData>>());
            }
            else if (menuItem.ItemId == Resource.Id.menuItem_navigationMenu_contacts)
            {
                _navigator.Go(App.Container.Resolve<IWay<ContactsViewModel, ContactsViewModel.Way.WayData>>());
            }
            else if (menuItem.ItemId == Resource.Id.menuItem_navigationMenu_about)
            {
                _navigator.Go(App.Container.Resolve<IWay<AboutViewModel, AboutViewModel.Way.WayData>>());
            }

            menuItem.SetChecked(true);

            DrawerLayout.CloseDrawer(DrawerGravity);

            return true;
        }
    }
}