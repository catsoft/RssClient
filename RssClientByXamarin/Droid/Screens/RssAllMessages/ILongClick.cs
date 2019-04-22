using System;

namespace Droid.Screens.RssAllMessages
{
    public interface ILongClick<T>
    {
        event EventHandler<T> LongClick;
    }
}