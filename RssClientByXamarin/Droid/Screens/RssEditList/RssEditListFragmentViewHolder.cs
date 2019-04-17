using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Droid.Screens.RssEditList
{
    public class RssEditListFragmentViewHolder
    {
        public RssEditListFragmentViewHolder(View view)
        {
            RecyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_rssEditList_list);
            RecyclerView.SetLayoutManager(new LinearLayoutManager(view.Context, LinearLayoutManager.Vertical, false));
            RecyclerView.AddItemDecoration(new DividerItemDecoration(view.Context, DividerItemDecoration.Vertical));
            RecyclerView.SaveEnabled = true;
            
            FloatingActionButton = view.FindViewById<FloatingActionButton>(Resource.Id.fab_rssEditList_addRss);
            EmptyEditText = view.FindViewById<TextView>(Resource.Id.textView_rssEditList_empty);
        }

        public RecyclerView RecyclerView { get; }

        public FloatingActionButton FloatingActionButton { get; }
        
        public TextView EmptyEditText{ get; }
    }
}