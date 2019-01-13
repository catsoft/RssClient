using Android.App;
using Android.OS;
using Android.Views;
using Droid.Screens.Base;
using Droid.Screens.Rss.List.RssAllMessagesList;
using Droid.Screens.Rss.List.RssList;
using Fragment = Android.Support.V4.App.Fragment;

namespace Droid.Screens.Rss.List
{
    [Activity(Label = "@string/all_appName", Theme = "@style/AppTheme.NoActionBar")]
    public class RssListActivity : ToolbarActivity
    {
        private int _containerId = Resource.Id.linearLayout_rssList_fragmentContainer;
        private Fragment _activeFragment;
        private readonly RssListFragment _rssListFragment = new RssListFragment();
        private readonly RssAllMessagesListFragment _rssAllMessagesListFragment = new RssAllMessagesListFragment();

        protected override int ResourceView => Resource.Layout.activity_rss_list;
        protected override bool IsDisplayHomeAsUpEnable => false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

			Title = GetText(Resource.String.rssList_titleActivity);

            SetFragment(_rssListFragment);
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
            transaction.Add(_containerId, fragment);

            transaction.Commit();
        }
    }
}