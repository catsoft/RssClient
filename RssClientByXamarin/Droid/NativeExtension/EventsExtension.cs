using System;
using System.Reactive;
using System.Reactive.Linq;
using Android.Widget;
using Droid.Screens.RssList;
using Shared.Services.Rss;
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
        
        public static IObservable<RssServiceModel> GetRssItemClickEvent(this RssListAdapter @this)
        {
            return Observable
                .FromEventPattern<RssServiceModel>(t => @this.Click += t, t => @this.Click -= t)
                .Select(_ => _.EventArgs);
        }
        
        public static IObservable<EventPattern<RssServiceModel>> GetRssItemLongClickEvent(this RssListAdapter @this)
        {
            return Observable.FromEventPattern<RssServiceModel>(t => @this.LongClick += t, t => @this.LongClick -= t);
        }
        
        public static IObservable<RssServiceModel> GetRssItemDismissEvent(this RssListAdapter @this)
        {
            return Observable
                .FromEventPattern<RssServiceModel>(t => @this.ItemDismiss += t, t => @this.ItemDismiss -= t)
                .Select(_ => _.EventArgs);
        }
    }
}