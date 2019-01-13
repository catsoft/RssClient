﻿using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.Screens.Rss.RssList;
using Realms;
using RssClient.Repository;
using Shared;

namespace Droid.Screens.Rss
{
    public class RssEditingListFragment : Fragment
    {
        private RecyclerView _recyclerView;
        private IRssRepository _rssRepository;

        public RssEditingListFragment()
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_rss_editing_list, container, false);

            _rssRepository = App.Container.Resolve<IRssRepository>();

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
    }
}