using System;
using System.Reactive.Linq;
using Android.Widget;

namespace Droid.NativeExtension
{
    public static class EventsExtension
    {
        public static IObservable<TextView.EditorActionEventArgs> GetEditorAction(this EditText @this)
        {
            return Observable
                .FromEventPattern<TextView.EditorActionEventArgs>(t => @this.EditorAction += t, t => @this.EditorAction -= t)
                .Select(_ => _.EventArgs);
        }
    }
}