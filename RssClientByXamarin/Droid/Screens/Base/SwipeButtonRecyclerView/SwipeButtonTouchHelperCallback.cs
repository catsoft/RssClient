using System;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Droid.Screens.Base.SwipeRecyclerView;

namespace Droid.Screens.Base.SwipeButtonRecyclerView
{
	public class SwipeButtonTouchHelperCallback : ItemTouchHelper.Callback
	{
		private readonly RecyclerView.Adapter _adapter;

		public SwipeButtonTouchHelperCallback(RecyclerView.Adapter adapter)
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

		public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder holder,
			RecyclerView.ViewHolder target)
		{
			return false;
		}

		public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction)
		{
		}

		private bool _swipeBack = false;

		public override int ConvertToAbsoluteDirection(int flags, int layoutDirection)
		{
			if (_swipeBack)
			{
				_swipeBack = false;
				return 0;
			}

			return base.ConvertToAbsoluteDirection(flags, layoutDirection);
		}

		public override void OnChildDraw(Canvas c, RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder,
			float dX, float dY, int actionState, bool isCurrentlyActive)
		{
			if (actionState == ItemTouchHelper.ActionStateSwipe)
			{
				SetTouchListener(c, recyclerView, viewHolder, dX, dY, actionState, isCurrentlyActive);
			}

			base.OnChildDraw(c, recyclerView, viewHolder, dX, dY, actionState, isCurrentlyActive);
		}

		private void SetTouchListener(Canvas c, RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, float dX, float dY, 
			int actionState, bool isCurrentlyActive)
		{
			
			recyclerView.SetOnTouchListener(new TouchList((value) => _swipeBack = value));

		}
		
		private class TouchList : Java.Lang.Object, View.IOnTouchListener
		{
			private readonly Action<bool> _action;

			public TouchList(Action<bool> func)
			{
				_action = func;
			}

			public bool OnTouch(View v, MotionEvent e)
			{
				_action.Invoke(e.Action == MotionEventActions.Cancel || e.Action == MotionEventActions.Up);
				return false;
			}
		}
	}
}