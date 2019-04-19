#region

using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using JetBrains.Annotations;

#endregion

namespace Droid.Screens.FeedlySearch
{
    public class FeedlySearchFragmentViewHolder
    {
        public FeedlySearchFragmentViewHolder([NotNull] View view)
        {
            RecyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_feedlySearch_list);
            RecyclerView.SetLayoutManager(new LinearLayoutManager(view.Context, LinearLayoutManager.Vertical, false));
            RecyclerView.AddItemDecoration(new DividerItemDecoration(view.Context, DividerItemDecoration.Vertical));

            ProgressBar = view.FindViewById<ProgressBar>(Resource.Id.progressBar_feedlySearch_progressBar);
            EmptyTextView = view.FindViewById<TextView>(Resource.Id.textView_feedlySearch_emptyText);
        }

        public RecyclerView RecyclerView { get; }

        public ProgressBar ProgressBar { get; }

        public TextView EmptyTextView { get; }
    }
}
