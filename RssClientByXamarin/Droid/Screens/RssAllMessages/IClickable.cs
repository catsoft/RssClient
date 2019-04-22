using System;

namespace Droid.Screens.RssAllMessages
{
    public interface IClickable<T>
    {
        event EventHandler<T> Click;
    }
}