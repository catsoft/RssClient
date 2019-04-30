using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.Messages.AllMessages
{
    public class AllMessagesFragmentViewHolder
    {
        public AllMessagesFragmentViewHolder([NotNull] View view)
        {
            FloatingActionButton = view.FindNotNull<FloatingActionButton>(Resource.Id.fab_allMessages_addRss);
            
            RecyclerView = view.FindNotNull<RecyclerView>(Resource.Id.recyclerView_allMessages_list);
            RecyclerView.SetLayoutManager(new LinearLayoutManager(view.Context, LinearLayoutManager.Vertical, false));
            RecyclerView.AddItemDecoration(new DividerItemDecoration(view.Context, DividerItemDecoration.Vertical));

            EmptyTextView = view.FindNotNull<TextView>(Resource.Id.textView_allMessages_emptyText);

            TopProgressBar = view.FindNotNull<ProgressBar>(Resource.Id.progressBar_allMessages_topProgressBar);
        }
        
        [NotNull] public RecyclerView RecyclerView { get; }
        
        [NotNull] public FloatingActionButton FloatingActionButton { get; }
        
        [NotNull] public TextView EmptyTextView { get; }
        
        [NotNull] public ProgressBar TopProgressBar { get; }
    }
}
