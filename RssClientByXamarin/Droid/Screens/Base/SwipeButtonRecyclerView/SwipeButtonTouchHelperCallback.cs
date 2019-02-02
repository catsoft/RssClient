using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Droid.Screens.Base.SwipeRecyclerView;

namespace Droid.Screens.Base.SwipeButtonRecyclerView
{
	public class SwipeButtonTouchHelperCallback : ItemTouchHelper.Callback
	{
		private readonly IItemTouchHelperAdapter _adapter;

		public SwipeButtonTouchHelperCallback(IItemTouchHelperAdapter adapter)
		{
			_adapter = adapter;
		}

		public override bool IsLongPressDragEnabled => false;
		public override bool IsItemViewSwipeEnabled => true;

		public override int GetMovementFlags(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder)
		{
			const int dragFlags = ItemTouchHelper.Up | ItemTouchHelper.Down;
			const int swipeFlags = ItemTouchHelper.Start | ItemTouchHelper.End;
			return MakeMovementFlags(dragFlags, swipeFlags);
		}

		public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder holder, RecyclerView.ViewHolder target)
		{
			return false;
		}

		public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction)
		{
			_adapter.OnItemDismiss(viewHolder.AdapterPosition);
		}
	}
}