using System;
using System.Reactive;
using System.Reactive.Linq;
using Android.Support.V4.Widget;
using Android.Widget;
using Droid.Screens.FeedlySearch;
using Droid.Screens.RssAllMessages;
using Droid.Screens.RssEditList;
using Droid.Screens.RssList;
using JetBrains.Annotations;
using Shared.Extensions;
using Shared.Repositories.Feedly;
using Shared.Services.Rss;
using Shared.ViewModels.RssListEdit;
using SearchView = Android.Support.V7.Widget.SearchView;

namespace Droid.NativeExtension
{
    public static class EventsExtension
    {
        [NotNull]
        public static IObservable<TextView.EditorActionEventArgs> GetEditorAction([NotNull] this EditText @this)
        {
            return Observable
                .FromEventPattern<TextView.EditorActionEventArgs>(t => @this.NotNull().EditorAction += t, t => @this.NotNull().EditorAction -= t)
                .Select(_ => _?.EventArgs);
        }

        [NotNull]
        public static IObservable<SearchView.QueryTextChangeEventArgs> GetQueryTextChangeEvent([NotNull] this SearchView @this)
        {
            return Observable
                .FromEventPattern<SearchView.QueryTextChangeEventArgs>(t => @this.NotNull().QueryTextChange += t,
                    t => @this.NotNull().QueryTextChange -= t)
                .Select(_ => _?.EventArgs);
        }

        [NotNull]
        public static IObservable<RssServiceModel> GetRssItemDismissEvent([NotNull] this RssListAdapter @this)
        {
            return Observable
                .FromEventPattern<RssServiceModel>(t => @this.NotNull().ItemDismiss += t, t => @this.NotNull().ItemDismiss -= t)
                .Select(_ => _?.EventArgs);
        }

        [NotNull]
        public static IObservable<RssServiceModel> GetItemDeleteEvent([NotNull] this RssListEditAdapter @this)
        {
            return Observable
                .FromEventPattern<RssServiceModel>(t => @this.NotNull().DeleteClick += t, t => @this.NotNull().DeleteClick -= t)
                .Select(_ => _?.EventArgs);
        }

        [NotNull]
        public static IObservable<MoveEventArgs> GetItemMoveEvent([NotNull] this RssListEditAdapter @this)
        {
            return Observable
                .FromEventPattern<MoveEventArgs>(t => @this.NotNull().OnMoveEvent += t, t => @this.NotNull().OnMoveEvent -= t)
                .Select(_ => _?.EventArgs);
        }

        [NotNull]
        public static IObservable<FeedlyRssDomainModel> GetClickAddImageEvent([NotNull] this FeedlySearchRssAdapter @this)
        {
            return Observable
                .FromEventPattern<FeedlyRssDomainModel>(t => @this.NotNull().ClickAddImage += t, t => @this.NotNull().ClickAddImage -= t)
                .Select(_ => _?.EventArgs);
        }

        [NotNull]
        public static IObservable<T> GetClickAction<T>([NotNull] this IClickable<T> @this)
            where T : class
        {
            return Observable
                .FromEventPattern<T>(t => @this.NotNull().Click += t, t => @this.NotNull().Click -= t)
                .Select(_ => _?.EventArgs);
        }

        [NotNull]
        public static IObservable<EventPattern<T>> GetLongClickAction<T>([NotNull] this ILongClick<T> @this)
            where T : class
        {
            return Observable
                .FromEventPattern<T>(t => @this.NotNull().LongClick += t, t => @this.NotNull().LongClick -= t);
        }

        [NotNull]
        public static IObservable<T> GetSwipeLeftAction<T>([NotNull] this ISwipeActions<T> @this)
            where T : class
        {
            return Observable
                .FromEventPattern<T>(t => @this.NotNull().LeftSwipeAction += t, t => @this.NotNull().LeftSwipeAction -= t)
                .Select(_ => _?.EventArgs);
        }

        [NotNull]
        public static IObservable<T> GetSwipeRightAction<T>([NotNull] this ISwipeActions<T> @this)
            where T : class
        {
            return Observable
                .FromEventPattern<T>(t => @this.NotNull().RightSwipeAction += t, t => @this.NotNull().RightSwipeAction -= t)
                .Select(_ => _?.EventArgs);
        }
        
        [NotNull]
        public static IObservable<object> GetRefreshAction([NotNull] this SwipeRefreshLayout @this)
        {
            return Observable
                .FromEventPattern(t => @this.NotNull().Refresh += t, t => @this.NotNull().Refresh -= t)
                .Select(_ => _?.EventArgs);
        }
    }
}
