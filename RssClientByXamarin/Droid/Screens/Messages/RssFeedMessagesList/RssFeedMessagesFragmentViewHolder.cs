using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Core.Extensions;
using JetBrains.Annotations;

namespace Droid.Screens.Messages.RssFeedMessagesList
{
    public class RssFeedMessagesFragmentViewHolder
    {
        public RssFeedMessagesFragmentViewHolder([NotNull] View view)
        {
            RecyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_rssDetail_messageList).NotNull();
            RecyclerView.SetLayoutManager(new LinearLayoutManager(view.Context, LinearLayoutManager.Vertical, false));
            RecyclerView.AddItemDecoration(new DividerItemDecoration(view.Context, DividerItemDecoration.Vertical));

            EmptyTextView = view.FindViewById<TextView>(Resource.Id.textView_rssList_emptyText);
            
            RefreshLayout = view.FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout_rssDetail_refresher);
        }

        [NotNull] public SwipeRefreshLayout RefreshLayout { get; }

        [NotNull] public RecyclerView RecyclerView { get; }
        
        [NotNull] public TextView EmptyTextView { get; }
    }
}
