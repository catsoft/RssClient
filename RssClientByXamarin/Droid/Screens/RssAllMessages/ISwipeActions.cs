using System;

namespace Droid.Screens.RssAllMessages
{
    public interface ISwipeActions<T>
    {
        event EventHandler<T> LeftSwipeAction;
        event EventHandler<T> RightSwipeAction;
    }
}