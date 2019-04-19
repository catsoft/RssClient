#region

using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;

#endregion

namespace Droid.Screens.Base.DragRecyclerView
{
    public class ReorderHelperCallback : ItemTouchHelper.Callback
    {
        private readonly IReorderListHelperAdapter _adapter;

        public ReorderHelperCallback(IReorderListHelperAdapter adapter) { _adapter = adapter; }

        public override bool IsLongPressDragEnabled => true;
        public override bool IsItemViewSwipeEnabled => false;

        public override int GetMovementFlags(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder)
        {
            const int dragFlags = ItemTouchHelper.Up | ItemTouchHelper.Down;
            const int swipeFlags = ItemTouchHelper.Start | ItemTouchHelper.End;
            return MakeMovementFlags(dragFlags, swipeFlags);
        }

        public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder holder, RecyclerView.ViewHolder target)
        {
            _adapter.OnMove(holder.AdapterPosition, target.AdapterPosition);

            return false;
        }

        public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction) { }
    }
}
