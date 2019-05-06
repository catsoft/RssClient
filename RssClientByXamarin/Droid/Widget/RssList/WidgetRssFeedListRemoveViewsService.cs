using Android;
using Android.App;
using Android.Content;
using Android.Widget;

namespace Droid.Widget.RssList
{
    [Service(Permission = Manifest.Permission.BindRemoteviews)]
    public class WidgetRssFeedListRemoveViewsService : RemoteViewsService
    {
        public const string WidgetId = "WidgetId";
        
        public override IRemoteViewsFactory OnGetViewFactory(Intent intent)
        {
            return new WidgetRssFeedListRemoteViewsFactory(ApplicationContext, intent.GetIntExtra(WidgetId, 0));
        }
    }
}