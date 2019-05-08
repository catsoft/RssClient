using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Droid.CustomView;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.Messages.AllMessages
{
    public class AllMessagesFragmentViewHolder
    {
        public AllMessagesFragmentViewHolder([NotNull] View view)
        {
            FloatingActionButton = view.FindNotNull<FloatingActionButton>(Resource.Id.fab_allMessages_addRss);
            ReadAllFloatingActionButton = view.FindNotNull<FloatingActionButton>(Resource.Id.fab_allMessages_readAll);
            
            RecyclerView = view.FindNotNull<RecyclerView>(Resource.Id.recyclerView_allMessages_list);
            RecyclerView.SetLayoutManager(new LinearLayoutManager(view.Context, LinearLayoutManager.Vertical, false));
            RecyclerView.AddItemDecoration(new DividerItemDecoration(view.Context, DividerItemDecoration.Vertical));

            EmptyTextView = view.FindNotNull<TextView>(Resource.Id.textView_allMessages_emptyText);

            TopProgressBar = view.FindNotNull<DrawableProgressBar>(Resource.Id.drawableProgressBar_allMessages_topProgressBar);
        }
        
        [NotNull] public RecyclerView RecyclerView { get; }
        
        [NotNull] public FloatingActionButton FloatingActionButton { get; }
        
        [NotNull] public FloatingActionButton ReadAllFloatingActionButton { get; }
        
        [NotNull] public TextView EmptyTextView { get; }
        
        [NotNull] public DrawableProgressBar TopProgressBar { get; }
    }
}
