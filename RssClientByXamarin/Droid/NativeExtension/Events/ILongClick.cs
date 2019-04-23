using System;

namespace Droid.NativeExtension.Events
{
    public interface ILongClick<T>
    {
        event EventHandler<T> LongClick;
    }
}