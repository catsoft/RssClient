using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Core.Extensions;
using Core.ViewModels.Main;
using Droid.NativeExtension;
using Droid.Screens.Navigation;

namespace Droid.Screens.Main
{
    [Activity(Label = "@string/all_appName", Theme = "@style/AppTheme.Default.NoActionBar")]
    public class MainActivity : FragmentActivity<MainViewModel>
    {
        protected override int ContainerId => Resource.Id.frameLayout_rssList_fragmentContainer;
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
                ViewModel.OpenRootScreenCommand.ExecuteIfCan();

                NavigationView.SetCheckedItem(Resource.Id.menuItem_navigationMenu_main);
            }
        }

        public override bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            if (menuItem.ItemId == Resource.Id.menuItem_navigationMenu_main)
                ViewModel.OpenRootScreenCommand.ExecuteIfCan();
            else if (menuItem.ItemId == Resource.Id.menuItem_navigationMenu_feedlySearch)
                ViewModel.OpenFeedlySearchCommand.ExecuteIfCan();
            else if (menuItem.ItemId == Resource.Id.menuItem_navigationMenu_favorite)
                ViewModel.OpenFavoriteMessagesCommand.ExecuteIfCan();
            else if (menuItem.ItemId == Resource.Id.menuItem_navigationMenu_settings)
                ViewModel.OpenSettingsCommand.ExecuteIfCan();
            else if (menuItem.ItemId == Resource.Id.menuItem_navigationMenu_contacts)
                ViewModel.OpenContactsCommand.ExecuteIfCan();
            else if (menuItem.ItemId == Resource.Id.menuItem_navigationMenu_rate)
                this.RateInMarket();
            //else if (menuItem.ItemId == Resource.Id.menuItem_navigationMenu_donate)
                //ViewModel.OpenDonateCommand.ExecuteIfCan();

            menuItem.SetChecked(true);

            DrawerLayout.CloseDrawer(DrawerGravity);

            return true;
        }
    }
}
