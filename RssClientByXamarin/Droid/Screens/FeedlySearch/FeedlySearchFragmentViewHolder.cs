using Android.Support.V7.Widget;
using Android.Views;

namespace Droid.Screens.FeedlySearch
{
    public class FeedlySearchFragmentViewHolder
    {
        private readonly View _view;

        public FeedlySearchFragmentViewHolder(View view)
        {
            _view = view;
            RecyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_feedlySearch_list);
            RecyclerView.SetLayoutManager(new LinearLayoutManager(view.Context, LinearLayoutManager.Vertical, false));
            RecyclerView.AddItemDecoration(new DividerItemDecoration(view.Context, DividerItemDecoration.Vertical));
        }

        public RecyclerView RecyclerView { get; }
    }
}