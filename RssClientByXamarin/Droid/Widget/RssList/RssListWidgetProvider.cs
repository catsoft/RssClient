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
//
//    [Service]
//    public class UpdateService : Service
//    {
//        public override void OnStart (Intent intent, int startId)
//        {
//            // Build the widget update for today
//            RemoteViews updateViews = buildUpdate (this);
//
//            // Push update for this widget to the home screen
//            ComponentName thisWidget = new ComponentName (this, Java.Lang.Class.FromType (typeof (WordWidget)).Name);
//            AppWidgetManager manager = AppWidgetManager.GetInstance (this);
//            manager.UpdateAppWidget (thisWidget, updateViews);
//        }
//
//        public override IBinder OnBind (Intent intent)
//        {
//            return null;
//        }
//        
//        public RemoteViews buildUpdate (Context context)
//        {
//            var entry = BlogPost.GetBlogPost ();
//
//            // Build an update that holds the updated widget contents
//            var updateViews = new RemoteViews (context.PackageName, Resource.Layout.widget_word);
//
//            updateViews.SetTextViewText (Resource.Id.blog_title, entry.Title);
//            updateViews.SetTextViewText (Resource.Id.creator, entry.Creator);
//
//            // When user clicks on widget, launch to Wiktionary definition page
//            if (!string.IsNullOrEmpty (entry.Link)) {
//                Intent defineIntent = new Intent (Intent.ActionView, Android.Net.Uri.Parse (entry.Link));
//			
//                PendingIntent pendingIntent = PendingIntent.GetActivity (context, 0, defineIntent, 0);
//                updateViews.SetOnClickPendingIntent (Resource.Id.widget, pendingIntent);
//            }
//
//            return updateViews;
//        }
//    }

}