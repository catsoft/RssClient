using Android;
using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.OS;
using Android.Widget;
using Droid.Screens.Main;

namespace Droid.Widgets.RssList
{
    [Service(Permission = Manifest.Permission.BindRemoteviews)]
    public class RssListWidgetUpdaterService : Service
    {
        public const string WidgetId = "WidgetId";

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            var widgetId = intent.GetIntExtra(WidgetId, 0);

            var updateViews = BuildUpdate(this, widgetId);
            
            var thisWidget = new ComponentName(this, Java.Lang.Class.FromType(typeof(RssListWidgetProvider)).Name);
            var manager = AppWidgetManager.GetInstance(this);
            var ids = manager.GetAppWidgetIds(thisWidget);
            manager.UpdateAppWidget(thisWidget, updateViews);
            manager.NotifyAppWidgetViewDataChanged(ids, Resource.Id.listView_widgetRssList_list);

            
            return StartCommandResult.NotSticky;
        }

        private RemoteViews BuildUpdate(Context context, int widgetId)
        {
            var updateViews = new RemoteViews(context.PackageName, Resource.Layout.widget_rss_list);

            SetListClick(updateViews, this);
            
            var adapterIntent = new Intent(context, typeof(WidgetRssFeedListRemoveViewsService));
            adapterIntent.PutExtra(WidgetRssFeedListRemoveViewsService.WidgetId, widgetId);
            updateViews.SetRemoteAdapter(Resource.Id.listView_widgetRssList_list, adapterIntent);
            updateViews.SetEmptyView(Resource.Id.listView_widgetRssList_list, Resource.Id.textView_widgetRssList_emptyText);
            
            return updateViews;
        }
        
        private void SetListClick(RemoteViews removeViews, Context context) 
        {
            var clickIntent = new Intent(context, typeof(MainActivity));
            var pendingIntentTemplate = PendingIntent.GetActivity(context, 0, clickIntent, 0);
            removeViews.SetPendingIntentTemplate(Resource.Id.listView_widgetRssList_list, pendingIntentTemplate);
        }
    }
}