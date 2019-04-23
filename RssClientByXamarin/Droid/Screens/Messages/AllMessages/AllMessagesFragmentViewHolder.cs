using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Core.Extensions;
using Droid.Resources;
using JetBrains.Annotations;

namespace Droid.Screens.Messages.AllMessages
{
    public class AllMessagesFragmentViewHolder
    {
        public AllMessagesFragmentViewHolder([NotNull] View view)
        {
            FloatingActionButton = view.FindViewById<FloatingActionButton>(Resource.Id.fab_allMessages_addRss).NotNull();
            
            RecyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_allMessages_list).NotNull();
            RecyclerView.SetLayoutManager(new LinearLayoutManager(view.Context, LinearLayoutManager.Vertical, false));
            RecyclerView.AddItemDecoration(new DividerItemDecoration(view.Context, DividerItemDecoration.Vertical));

            EmptyTextView = view.FindViewById<TextView>(Resource.Id.textView_allMessagesFilter_emptyText);
        }
        
        [NotNull] public RecyclerView RecyclerView { get; }
        
        [NotNull] public FloatingActionButton FloatingActionButton { get; }
        
        [NotNull] public TextView EmptyTextView { get; }
    }
}
