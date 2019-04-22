using System;

namespace Droid.Screens.Base.SwipeButtonRecyclerView
{
    public interface ISwipeButtonItemTouchHelperViewHolder
    {
        bool IsLeftButton { get; }
        bool IsRightButton { get; }

        string LeftButtonText { get; }
        string RightButtonText { get; }

        event Action LeftButtonAction;
        event Action RightButtonAction;

        void OnLeftButton();
        void OnRightButton();
    }
}
