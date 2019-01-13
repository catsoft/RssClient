using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Autofac;
using Droid.Screens.Navigation;
using Droid.Screens.Rss.Contacts;
using Droid.Screens.Rss.RssAllMessagesList;
using Droid.Screens.Rss.RssList;
using Droid.Screens.Rss.Settings;
using Shared;
using Shared.Services.Navigator;
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

        private readonly int _containerId = Resource.Id.linearLayout_rssList_fragmentContainer;
        private Fragment _activeFragment;

        protected override int ResourceView => Resource.Layout.activity_rss_list;
        public static Activity Instance { get; private set; }

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

            SetFragment(_rssListFragment);

            NavigationView.SetCheckedItem(_mainId);
        }

        public override bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            var navigator = App.Container.Resolve<INavigator>();
            navigator.GoBack();

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

            SetFragment(fragment);

            menuItem.SetChecked(true);

            DrawerLayout.CloseDrawer(DrawerGravity);

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.menuItem_rssList_change)
            {
                ChangeFragment();
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void ChangeFragment()
        {
            var manager = SupportFragmentManager;
            var transaction = manager.BeginTransaction();

            var otherFragment = _activeFragment == _rssListFragment ? (Fragment)_rssAllMessagesListFragment : _rssListFragment;
            _activeFragment = otherFragment;
            transaction.Replace(_containerId, otherFragment);

            transaction.Commit();
        }

        private void SetFragment(Fragment fragment)
        {
            var manager = SupportFragmentManager;
            var transaction = manager.BeginTransaction();

            _activeFragment = fragment;
            transaction.Replace(_containerId, fragment);

            transaction.Commit();
        }
    }
}