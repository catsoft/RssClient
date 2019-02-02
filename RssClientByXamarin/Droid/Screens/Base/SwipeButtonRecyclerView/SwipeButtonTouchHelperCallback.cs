using System;
using System.Runtime.CompilerServices;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Droid.Screens.Base.SwipeRecyclerView;

namespace Droid.Screens.Base.SwipeButtonRecyclerView
{
	public class SwipeButtonTouchHelperCallback : ItemTouchHelper.Callback
	{
		private bool _swipeBack;
		private bool _isActionInvoke;

		private const float ButtonWidth = 300;

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
		}

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

			if (viewHolder is SwipeButtonViewHolder swipeViewHolder)
			{
				if (dX != 0)
				{
					DrawButtons(c, swipeViewHolder, (int) dX);
				}
				else
				{
					_isActionInvoke = false;
				}

				if (dX < 0)
				{
					if (!_isActionInvoke)
					{
						swipeViewHolder.OnRightButton();
						_isActionInvoke = true;
					}
				}

				if (dX > 0)
				{
					if (!_isActionInvoke)
					{
						swipeViewHolder.OnLeftButton();
						_isActionInvoke = true;
					}
				}
			}


			base.OnChildDraw(c, recyclerView, viewHolder, dX, dY, actionState, isCurrentlyActive);
		}

		private void DrawButtons(Canvas c, SwipeButtonViewHolder viewHolder, int dX)
		{
			var itemView = viewHolder.ItemView;
			var p = new Paint();

			if (viewHolder.IsLeftButton && dX > ButtonWidth)
			{
				var leftButton = new RectF(itemView.Left, itemView.Top, itemView.Left + ButtonWidth, itemView.Bottom);
				p.Color = Color.Blue;
				c.DrawRoundRect(leftButton, 0, 0, p);
				DrawText(viewHolder.LeftButtonText, c, leftButton, p);
			}

			if (viewHolder.IsRightButton && dX < -ButtonWidth)
			{
				var rightButton = new RectF(itemView.Right - ButtonWidth, itemView.Top, itemView.Right, itemView.Bottom);
				p.Color = Color.Red;
				c.DrawRoundRect(rightButton, 0, 0, p);
				DrawText(viewHolder.RightButtonText, c, rightButton, p);
			}
		}

		private void DrawText(string text, Canvas c, RectF button, Paint p)
		{
			const float textSize = 60;
			p.Color = Color.White;
			p.AntiAlias = true;
			p.TextSize = textSize;

			var textWidth = p.MeasureText(text);
			c.DrawText(text, button.CenterX() - (textWidth / 2), button.CenterY() + (textSize / 2), p);
		}


		private void SetTouchListener(Canvas c, RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, float dX,
			float dY, int actionState, bool isCurrentlyActive)
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