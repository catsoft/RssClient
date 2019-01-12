using System;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.Screens.Rss.Create;
using Realms;
using RssClient.Repository;
using Shared;

namespace Droid.Screens.Rss.List
{
    public class RssListFragment : Fragment
    {
        private RecyclerView _recyclerView;
        private IRssRepository _rssRepository;

        public RssListFragment()
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_rss_list, container, false);

            _rssRepository = App.Container.Resolve<IRssRepository>();

            var fab = view.FindViewById<FloatingActionButton>(Resource.Id.fab_rssList_addRss);
            fab.Click += FabOnClick;

            _recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_rssList_list);
            _recyclerView.SetLayoutManager(new LinearLayoutManager(Context, LinearLayoutManager.Vertical, false));

            var items = _rssRepository.GetList();
            var adapter = new RssListAdapter(items, Activity);
            _recyclerView.SetAdapter(adapter);
            adapter.NotifyDataSetChanged();

            items.SubscribeForNotifications((sender, changes, error) =>
            {
                if (sender != null && changes != null)
                {
                    foreach (var changesInsertedIndex in changes.InsertedIndices)
                    {
                        adapter.NotifyItemInserted(changesInsertedIndex);
                    }

                    foreach (var changesInsertedIndex in changes.ModifiedIndices)
                    {
                        adapter.NotifyItemChanged(changesInsertedIndex);
                    }

                    foreach (var changesInsertedIndex in changes.DeletedIndices)
                    {
                        adapter.NotifyItemRemoved(changesInsertedIndex);
                    }
                }

                adapter.NotifyDataSetChanged();
            });

            return view;
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            var intent = new Intent(Context, typeof(RssCreateActivity));
            StartActivity(intent);
        }
    }
}