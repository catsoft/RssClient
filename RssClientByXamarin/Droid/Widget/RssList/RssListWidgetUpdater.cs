using Android.App;
using Android.Appwidget;
using Android.Content;
using Core.Infrastructure.Widgets;

namespace Droid.Widget.RssList
{
    public class RssListWidgetUpdater : IRssListWidgetUpdater
    {
        public void Update()
        {
            var context = Application.Context;
            var intent = new Intent(context, typeof(RssListWidgetProvider));
            intent.SetAction(AppWidgetManager.ActionAppwidgetUpdate);
            var componentName = new ComponentName (context, Java.Lang.Class.FromType (typeof (RssListWidgetProvider)).Name);
            var ids = AppWidgetManager.GetInstance(context).GetAppWidgetIds(componentName);
            intent.PutExtra(AppWidgetManager.ExtraAppwidgetIds, ids);
            context.SendBroadcast(intent);
        }
    }
}