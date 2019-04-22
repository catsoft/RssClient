using System;
using Android.Support.V7.Widget;
using Android.Views;

namespace Droid.Screens.Base.SwipeButtonRecyclerView
{
    public abstract class SwipeButtonViewHolder : RecyclerView.ViewHolder, ISwipeButtonItemTouchHelperViewHolder
    {
        protected SwipeButtonViewHolder(View itemView) : base(itemView) { }

        public abstract bool IsLeftButton { get; }
        public abstract bool IsRightButton { get; }
        public abstract string LeftButtonText { get; }
        public abstract string RightButtonText { get; }

        public event Action LeftButtonAction;
        public event Action RightButtonAction;

        public void OnLeftButton() { LeftButtonAction?.Invoke(); }

        public void OnRightButton() { RightButtonAction?.Invoke(); }
    }
}
