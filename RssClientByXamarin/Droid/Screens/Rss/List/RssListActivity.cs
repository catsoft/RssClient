using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Widget;
using Autofac;
using Droid.Screens.Base;
using Droid.Screens.Rss.Create;
using Realms;
using RssClient;
using RssClient.Repository;
using Shared;

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

            SetListFragment();
        }

        private void SetListFragment()
        {
            var manager = SupportFragmentManager;
            var transaction = manager.BeginTransaction();

            transaction.Add(_containerId, new RssListFragment());

            transaction.Commit();
        }
    }
}