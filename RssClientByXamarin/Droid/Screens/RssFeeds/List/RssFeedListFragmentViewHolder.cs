using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Droid.CustomView;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.RssFeeds.List
{
    public class RssFeedListFragmentViewHolder
    {
        public RssFeedListFragmentViewHolder(View view)
        {
            FloatingActionButton = view.FindNotNull<FloatingActionButton>(Resource.Id.fab_rssList_addRss);

            RecyclerView = view.FindNotNull<RecyclerView>(Resource.Id.recyclerView_rssList_list);
            RecyclerView.SetLayoutManager(new LinearLayoutManager(view.Context, LinearLayoutManager.Vertical, false));

            EmptyTextView = view.FindNotNull<TextView>(Resource.Id.textView_rssList_emptyText);

            TopProgressBar = view.FindNotNull<DrawableProgressBar>(Resource.Id.drawableProgressBar_rssList_topProgressBar);
        }

        [NotNull] public FloatingActionButton FloatingActionButton { get; }

        [NotNull] public RecyclerView RecyclerView { get; }

        [NotNull] public TextView EmptyTextView { get; }
        
        [NotNull] public DrawableProgressBar TopProgressBar { get; }
    }
}
