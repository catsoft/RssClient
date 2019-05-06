using System;
using System.Collections.Generic;
using Android.Content;
using Android.Widget;
using Core.Analytics;
using Core.Analytics.Rss;
using Core.Api.RssFeeds;
using Core.CoreServices.Html;
using Core.Database;
using Core.Infrastructure.Locale;
using Core.Repositories.Configurations;
using Core.Repositories.RssFeeds;
using Core.Repositories.RssMessage;
using Core.Resources;
using Core.Services.RssFeeds;
using Square.Picasso;
using Exception = Java.Lang.Exception;

namespace Droid.Widget.RssList
{
    public class WidgetRssFeedListRemoteViewsFactory : Java.Lang.Object, RemoteViewsService.IRemoteViewsFactory
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly int _widgetId;
        private readonly Context _context;
        private readonly List<RssFeedServiceModel> _list;
        private RssFeedService _rssService;

        public WidgetRssFeedListRemoteViewsFactory(Context context, int widgetId)
        {
            _context = context;
            _widgetId = widgetId;
            _list = new List<RssFeedServiceModel>();
        }
        
        public long GetItemId(int position)
        {
            return position;
        }
        
        public RemoteViews GetViewAt(int position)
        {
            var itemView = new RemoteViews(_context.PackageName, Resource.Layout.widget_list_item_rss);

            var item = _list[position];
            
            var subTitle = item.UpdateTime == null
                ? Strings.RssFeedItemNotUpdated
                : $"{Strings.RssFeedItemUpdated} {item.UpdateTime.Value.ToShortGeneralLocaleString()}";
            var countMessages = item.CountNewMessages.ToString();
            
            itemView.SetTextViewText(Resource.Id.textView_widgetListItemRss_title, item.Name);
            itemView.SetTextViewText(Resource.Id.textView_widgetListItemRss_subtitle, subTitle);
            itemView.SetTextViewText(Resource.Id.textView_widgetListItemRss_rssCount, countMessages);
            
            if (!string.IsNullOrEmpty(item.UrlPreviewImage))
            {
                try
                {
                    var picture = Picasso.With(_context).Load(item.UrlPreviewImage).Get();
                    itemView.SetImageViewBitmap(Resource.Id.imageView_widgetListItemRss_rssIcon, picture);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return itemView;
        }
        
        public void OnCreate()
        {
            var log = new Log();
            var database = new SqliteDatabase(new Log());
            var mapper = new RssFeedMapper();
            var configRepo = new ConfigurationRepository(database);
            var messageMapper = new RssMessageMapper(new HtmlConfigurator(configRepo));
            var messageRep = new RssMessagesRepository(database, configRepo, messageMapper, messageMapper);
            var apiClient = new RssFeedApiClient(log);
            var feedRepo = new RssFeedRepository(database, new RssLog(log), mapper, mapper);
            _rssService = new RssFeedService(feedRepo, mapper, mapper, apiClient, messageRep);
        }

        public void OnDataSetChanged()
        {
            _list.Clear();
            var task = _rssService.GetListAsync();
            task.Wait();
            var items = task.Result;
            _list.AddRange(items);
        }

        public void OnDestroy()
        {
        }

        public int Count => _list.Count;
        
        public bool HasStableIds => true;
        
        public RemoteViews LoadingView => null;
        
        public int ViewTypeCount => 1;
    }
}