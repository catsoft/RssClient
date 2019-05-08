using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.Messages.RssFeedMessagesList
{
    public class RssFeedMessagesFragmentViewHolder
    {
        public RssFeedMessagesFragmentViewHolder([NotNull] View view)
        {
            RecyclerView = view.FindNotNull<RecyclerView>(Resource.Id.recyclerView_rssDetail_messageList);
            RecyclerView.SetLayoutManager(new LinearLayoutManager(view.Context, LinearLayoutManager.Vertical, false));
            RecyclerView.AddItemDecoration(new DividerItemDecoration(view.Context, DividerItemDecoration.Vertical));

            EmptyTextView = view.FindNotNull<TextView>(Resource.Id.textView_rssFeedMessageList_emptyText);
            
            RefreshLayout = view.FindNotNull<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout_rssDetail_refresher);

            ReadAllFloatingActionButton = view.FindNotNull<FloatingActionButton>(Resource.Id.fab_rssFeedMessageList_readAll);
        }

        [NotNull] public SwipeRefreshLayout RefreshLayout { get; }

        [NotNull] public RecyclerView RecyclerView { get; }
        
        [NotNull] public TextView EmptyTextView { get; }
        
        [NotNull] public FloatingActionButton ReadAllFloatingActionButton { get; }
    }
}
