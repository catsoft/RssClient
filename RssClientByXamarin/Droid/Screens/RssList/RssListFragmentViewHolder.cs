using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Droid.Screens.RssList
{
    public class RssListFragmentViewHolder
    {
        public RssListFragmentViewHolder(View view)
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