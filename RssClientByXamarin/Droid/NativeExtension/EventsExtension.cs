using System;
using System.Reactive.Linq;
using Android.Widget;
using SearchView = Android.Support.V7.Widget.SearchView;

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
        
        public static IObservable<SearchView.QueryTextChangeEventArgs> GetQueryTextChangeEvent(this SearchView @this)
        {
            return Observable
                .FromEventPattern<SearchView.QueryTextChangeEventArgs>(t => @this.QueryTextChange += t, t => @this.QueryTextChange -= t)
                .Select(_ => _.EventArgs);
        }
    }
}