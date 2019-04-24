using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Core.Extensions;
using JetBrains.Annotations;

namespace Droid.Screens.Messages.FavoriteMessagesList
{
    public class RssFavoriteMessagesListFragmentViewHolder
    {
        public RssFavoriteMessagesListFragmentViewHolder([NotNull] View view)
        {
            RecyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_favoriteMessages_list).NotNull();
            
            RecyclerView.SetLayoutManager(new LinearLayoutManager(view.Context, LinearLayoutManager.Vertical, false));
            RecyclerView.AddItemDecoration(new DividerItemDecoration(view.Context, DividerItemDecoration.Vertical));

            EmptyTextView = view.FindViewById<TextView>(Resource.Id.textview_favoriteMessages_empty).NotNull();
        }
        
        [NotNull] public RecyclerView RecyclerView { get; }
        
        [NotNull] public TextView EmptyTextView { get; }
    }
}