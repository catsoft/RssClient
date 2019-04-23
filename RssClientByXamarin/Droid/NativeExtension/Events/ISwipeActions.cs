using System;

namespace Droid.NativeExtension.Events
{
    public interface ISwipeActions<T>
    {
        event EventHandler<T> LeftSwipeAction;
        event EventHandler<T> RightSwipeAction;
    }
}