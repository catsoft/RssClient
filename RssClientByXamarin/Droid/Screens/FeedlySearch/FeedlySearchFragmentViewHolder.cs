using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.FeedlySearch
{
    public class FeedlySearchFragmentViewHolder
    {
        public FeedlySearchFragmentViewHolder([NotNull] View view)
        {
            RecyclerView = view.FindNotNull<RecyclerView>(Resource.Id.recyclerView_feedlySearch_list);
            RecyclerView.SetLayoutManager(new LinearLayoutManager(view.Context, LinearLayoutManager.Vertical, false));
            RecyclerView.AddItemDecoration(new DividerItemDecoration(view.Context, DividerItemDecoration.Vertical));

            ProgressBar = view.FindNotNull<ProgressBar>(Resource.Id.progressBar_feedlySearch_progressBar);
            EmptyTextView = view.FindNotNull<TextView>(Resource.Id.textView_feedlySearch_emptyText);
        }

        [NotNull] public RecyclerView RecyclerView { get; }

        [NotNull] public ProgressBar ProgressBar { get; }

        [NotNull] public TextView EmptyTextView { get; }
    }
}
