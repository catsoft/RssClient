using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using JetBrains.Annotations;
using Shared.Extensions;

namespace Droid.Screens.RssEditList
{
    public class RssEditListFragmentViewHolder
    {
        public RssEditListFragmentViewHolder([NotNull] View view)
        {
            RecyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_rssEditList_list).NotNull();
            RecyclerView.SetLayoutManager(new LinearLayoutManager(view.Context, LinearLayoutManager.Vertical, false));
            RecyclerView.AddItemDecoration(new DividerItemDecoration(view.Context, DividerItemDecoration.Vertical));
            RecyclerView.SaveEnabled = true;

            FloatingActionButton = view.FindViewById<FloatingActionButton>(Resource.Id.fab_rssEditList_addRss).NotNull();
            EmptyEditText = view.FindViewById<TextView>(Resource.Id.textView_rssEditList_empty).NotNull();
        }

        [NotNull] public RecyclerView RecyclerView { get; }

        [NotNull] public FloatingActionButton FloatingActionButton { get; }

        [NotNull] public TextView EmptyEditText { get; }
    }
}
