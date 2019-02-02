using System;

namespace Droid.Screens.Base.SwipeButtonRecyclerView
{
    public interface ISwipeButtonItemTouchHelperAdapter
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