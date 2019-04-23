using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Droid.Resources;

namespace Droid.Screens.RssFeeds.List
{
    public class RssFeedListFragmentViewHolder
    {
        public RssFeedListFragmentViewHolder(View view)
        {
            FloatingActionButton = view.FindViewById<FloatingActionButton>(Resource.Id.fab_rssList_addRss);

            RecyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_rssList_list);
            RecyclerView.SetLayoutManager(new LinearLayoutManager(view.Context, LinearLayoutManager.Vertical, false));

            EmptyTextView = view.FindViewById<TextView>(Resource.Id.textView_rssList_emptyText);
        }

        public FloatingActionButton FloatingActionButton { get; }

        public RecyclerView RecyclerView { get; }

        public TextView EmptyTextView { get; }
    }
}
