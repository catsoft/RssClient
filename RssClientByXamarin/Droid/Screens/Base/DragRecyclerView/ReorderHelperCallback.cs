using System.Collections.Generic;
using Android.App;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Droid.Screens.Base.Adapters;

namespace Droid.Screens.Base.DragRecyclerView
{
	public interface IDragListener
	{
		void OnStartDrag(RecyclerView.ViewHolder holder);
	}
	
	public abstract class ReorderRecyclerViewAdapter<TItems, TCollection> : WithItemsAdapter<TItems, TCollection>, IReorderListHelperAdapter
		where TCollection : IEnumerable<TItems>
	{
		protected ReorderRecyclerViewAdapter(TCollection items, Activity activity) : base(items, activity)
		{
		}

		public abstract void OnMove(int fromPosition, int toPosition);
	}
	
	public interface IReorderListHelperAdapter
	{
		void OnMove(int fromPosition, int toPosition);
	}
	
	public class ReorderHelperCallback : ItemTouchHelper.Callback
	{
		private readonly IReorderListHelperAdapter _adapter;

		public ReorderHelperCallback(IReorderListHelperAdapter adapter)
		{
			_adapter = adapter;
		}

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

		public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction)
		{
		}
	}
}