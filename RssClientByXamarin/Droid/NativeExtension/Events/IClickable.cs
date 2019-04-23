using System;

namespace Droid.NativeExtension.Events
{
    public interface IClickable<T>
    {
        event EventHandler<T> Click;
    }
}