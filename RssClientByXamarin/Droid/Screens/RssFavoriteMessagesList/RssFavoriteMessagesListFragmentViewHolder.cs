using Android.Support.V7.Widget;
using Android.Views;
using JetBrains.Annotations;
using Shared.Extensions;

namespace Droid.Screens.RssFavoriteMessagesList
{
    public class RssFavoriteMessagesListFragmentViewHolder
    {
        public RssFavoriteMessagesListFragmentViewHolder([NotNull] View view)
        {
            RecyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_favoriteMessages_list).NotNull();
            
            RecyclerView.SetLayoutManager(new LinearLayoutManager(view.Context, LinearLayoutManager.Vertical, false));
            RecyclerView.AddItemDecoration(new DividerItemDecoration(view.Context, DividerItemDecoration.Vertical));
        }
        
        [NotNull] public RecyclerView RecyclerView { get; }
    }
}