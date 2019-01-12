using Android.App;
using Android.OS;
using Android.Views;
using Droid.Screens.Base;
using Fragment = Android.Support.V4.App.Fragment;

namespace Droid.Screens.Rss.List
{
    [Activity(Label = "@string/all_appName", Theme = "@style/AppTheme.NoActionBar")]
    public class RssListActivity : ToolbarActivity
    {
        private int _containerId = Resource.Id.linearLayout_rssList_fragmentContainer;

        protected override int ResourceView => Resource.Layout.activity_rss_list;
        protected override bool IsDisplayHomeAsUpEnable => false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

			Title = GetText(Resource.String.rssList_titleActivity);

            SetFragment(new RssListFragment());
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.menuItem_rssList_change)
            {
                SetFragment(new RssAllMessagesListFragment());
                return true;
            }

            if (item.ItemId == Resource.Id.menuItem_rssList_edit)
            {
                SetFragment(new RssEditingListFragment());
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void SetFragment(Fragment fragment)
        {
            var manager = SupportFragmentManager;
            var transaction = manager.BeginTransaction();

            transaction.Replace(_containerId, fragment);

            transaction.Commit();
        }
    }
}