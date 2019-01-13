using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Droid.Screens.Contacts;
using Droid.Screens.Navigation;
using Droid.Screens.RssAllMessagesList;
using Droid.Screens.RssList;
using Droid.Screens.Settings;
using Fragment = Android.Support.V4.App.Fragment;

namespace Droid.Screens.Main
{
    [Activity(Label = "@string/all_appName", Theme = "@style/AppTheme.NoActionBar")]
    public class MainActivity : BurgerActivity
    {
        private readonly int _mainId = Resource.Id.menuItem_navigationMenu_main;
        private readonly int _settingsId = Resource.Id.menuItem_navigationMenu_settings;
        private readonly int _contactsId = Resource.Id.menuItem_navigationMenu_contacts;

        private readonly SettingsFragment _settingsFragment = new SettingsFragment();
        private readonly ContactsFragment _contactFragment = new ContactsFragment();
        private readonly RssListFragment _rssListFragment = new RssListFragment();
        private readonly RssAllMessagesListFragment _rssAllMessagesListFragment = new RssAllMessagesListFragment();

        protected override int? ContainerId => Resource.Id.linearLayout_rssList_fragmentContainer;
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

            base.OnCreate(savedInstanceState);

            Title = GetText(Resource.String.rssList_title);

            AddFragment(_rssListFragment);

            NavigationView.SetCheckedItem(_mainId);
        }

        public override bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            Fragment fragment = null;

            if (menuItem.ItemId == _mainId)
            {
                fragment = _rssListFragment;
            } else if (menuItem.ItemId == _settingsId)
            {
                fragment = _settingsFragment;
            }
            else if (menuItem.ItemId == _contactsId)
            {
                fragment = _contactFragment;
            }

            AddFragment(fragment);

            menuItem.SetChecked(true);

            DrawerLayout.CloseDrawer(DrawerGravity);

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.menuItem_rssList_change)
            {
                var otherFragment = ActiveFragment == _rssListFragment ? (Fragment)_rssAllMessagesListFragment : _rssListFragment;
                AddFragment(otherFragment);
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}