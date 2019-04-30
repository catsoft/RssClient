using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.Messages.FavoriteMessagesList
{
    public class RssFavoriteMessagesListFragmentViewHolder
    {
        public RssFavoriteMessagesListFragmentViewHolder([NotNull] View view)
        {
            RecyclerView = view.FindNotNull<RecyclerView>(Resource.Id.recyclerView_favoriteMessages_list);
            
            RecyclerView.SetLayoutManager(new LinearLayoutManager(view.Context, LinearLayoutManager.Vertical, false));
            RecyclerView.AddItemDecoration(new DividerItemDecoration(view.Context, DividerItemDecoration.Vertical));

            EmptyTextView = view.FindNotNull<TextView>(Resource.Id.textview_favoriteMessages_empty);
        }
        
        [NotNull] public RecyclerView RecyclerView { get; }
        
        [NotNull] public TextView EmptyTextView { get; }
    }
}