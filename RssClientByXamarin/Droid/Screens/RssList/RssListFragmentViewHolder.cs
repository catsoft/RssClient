using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;

namespace Droid.Screens.RssList
{
    public class RssListFragmentViewHolder
    {
        public RssListFragmentViewHolder(View view)
        {
                    
            FloatingActionButton = view.FindViewById<FloatingActionButton>(Resource.Id.fab_rssList_addRss);
            
            RecyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_rssList_list);
            RecyclerView.SetLayoutManager(new LinearLayoutManager(view.Context, LinearLayoutManager.Vertical, false));
        }

        public FloatingActionButton FloatingActionButton { get; }
        
        public RecyclerView RecyclerView { get; }
    }
}