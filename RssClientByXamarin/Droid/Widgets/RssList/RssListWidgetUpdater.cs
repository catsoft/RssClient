using Android.App;
using Android.Appwidget;
using Android.Content;
using Core.Extensions;
using Core.Infrastructure.Widgets;

namespace Droid.Widgets.RssList
{
    public class RssListWidgetUpdater : IRssListWidgetUpdater
    {
        public void Update()
        {
            var context = Application.Context.NotNull();
            var intent = new Intent(context, typeof(RssListWidgetProvider));
            intent.SetAction(AppWidgetManager.ActionAppwidgetUpdate);
            var componentName = new ComponentName(context, Java.Lang.Class.FromType(typeof(RssListWidgetProvider)).NotNull().Name);
            var ids = AppWidgetManager.GetInstance(context).NotNull().GetAppWidgetIds(componentName);
            intent.PutExtra(AppWidgetManager.ExtraAppwidgetIds, ids);
            context.SendBroadcast(intent);
        }
    }
}