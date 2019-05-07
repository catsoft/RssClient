using System;
using System.Collections.Generic;
using Android.Content;
using Android.Widget;
using Core;
using Core.Extensions;
using Core.Infrastructure.Locale;
using Core.Resources;
using Core.Services.RssFeeds;
using JetBrains.Annotations;
using Square.Picasso;
using Exception = Java.Lang.Exception;

namespace Droid.Widgets.RssList
{
    public class WidgetRssFeedListRemoteViewsFactory : Java.Lang.Object, RemoteViewsService.IRemoteViewsFactory
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly int _widgetId;
        [NotNull] private readonly Context _context;
        [NotNull] private readonly List<RssFeedServiceModel> _list;
        private IRssFeedService _rssService;

        public WidgetRssFeedListRemoteViewsFactory([NotNull] Context context, int widgetId)
        {
            _context = context;
            _widgetId = widgetId;
            _list = new List<RssFeedServiceModel>();
        }

        public long GetItemId(int position) { return position; }

        public RemoteViews GetViewAt(int position)
        {
            var remoteViews = new RemoteViews(_context.PackageName, Resource.Layout.widget_list_item_rss);

            var item = _list[position].NotNull();

            var subTitle = item.UpdateTime == null
                ? Strings.RssFeedItemNotUpdated
                : $"{Strings.RssFeedItemUpdated} {item.UpdateTime.Value.ToShortGeneralLocaleString()}";
            var countMessages = item.CountNewMessages.ToString();

            remoteViews.SetTextViewText(Resource.Id.textView_widgetListItemRss_title, item.Name);
            remoteViews.SetTextViewText(Resource.Id.textView_widgetListItemRss_subtitle, subTitle);
            remoteViews.SetTextViewText(Resource.Id.textView_widgetListItemRss_rssCount, countMessages);

            if (!string.IsNullOrEmpty(item.UrlPreviewImage))
                try
                {
                    var picture = Picasso.With(_context).NotNull().Load(item.UrlPreviewImage).NotNull().Get();
                    remoteViews.SetImageViewBitmap(Resource.Id.imageView_widgetListItemRss_rssIcon, picture);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            var clickIntent = new Intent();
            clickIntent.PutExtra("IdGuid", item.Id.ToString());
            remoteViews.SetOnClickFillInIntent(Resource.Id.linearLayout_widgetListItemRss_content, clickIntent);

            return remoteViews;
        }

        public void OnCreate() { _rssService = App.BuildRssFeedService(); }

        public void OnDataSetChanged()
        {
            _list.Clear();
            var task = _rssService.NotNull().GetListAsync();
            task.Wait();
            var items = task.Result;
            _list.AddRange(items);
        }

        public void OnDestroy() { }

        public int Count => _list.Count;

        public bool HasStableIds => true;

        public RemoteViews LoadingView => null;

        public int ViewTypeCount => 1;
    }
}