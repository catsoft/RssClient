using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Core.Extensions;
using JetBrains.Annotations;

namespace Droid.Screens.RssFeeds.List
{
    public class RssFeedListFragmentViewHolder
    {
        public RssFeedListFragmentViewHolder(View view)
        {
            FloatingActionButton = view.FindViewById<FloatingActionButton>(Resource.Id.fab_rssList_addRss).NotNull();

            RecyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_rssList_list).NotNull();
            RecyclerView.SetLayoutManager(new LinearLayoutManager(view.Context, LinearLayoutManager.Vertical, false));

            EmptyTextView = view.FindViewById<TextView>(Resource.Id.textView_rssList_emptyText).NotNull();

            TopProgressBar = view.FindViewById<ProgressBar>(Resource.Id.progressBar_rssList_topProgressBar).NotNull();
        }

        [NotNull] public FloatingActionButton FloatingActionButton { get; }

        [NotNull] public RecyclerView RecyclerView { get; }

        [NotNull] public TextView EmptyTextView { get; }
        
        [NotNull] public ProgressBar TopProgressBar { get; }
    }
}
