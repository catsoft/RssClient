using Android.App;
using Android.Appwidget;
using Android.Content;

namespace Droid.Widget.RssList
{
    [BroadcastReceiver(Label = "@string/RssListWidgetTitle")]
    [IntentFilter(new string[] { "android.appwidget.action.APPWIDGET_UPDATE" })]
    [MetaData("android.appwidget.provider", Resource = "@xml/widget_rss_list_provider")]
    public class RssListWidgetProvider : AppWidgetProvider
    {
        public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
        {
            base.OnUpdate(context, appWidgetManager, appWidgetIds);
        }

        public override void OnReceive(Context context, Intent intent)
        {
            base.OnReceive(context, intent);
        }
    }
}