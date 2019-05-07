using Android.App;
using Android.Content;
using Android.OS;

namespace Droid.Infrastructure.Alarm
{
    public class RssRssAlarmManager : IRssAlarmManager
    {
        public void InitAlarm<T>(Context context, int interval)
        {
            var intent = new Intent(context, typeof(T));
            var pendingIntent = PendingIntent.GetService(context, 0, intent, PendingIntentFlags.UpdateCurrent);
            var alarmManager = context.GetSystemService(Context.AlarmService) as AlarmManager;
            alarmManager?.SetRepeating(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + interval, interval, pendingIntent);
        }

        public void RemoveAlarm<T>(Context context)
        {
            var intent = new Intent(context, typeof(T));
            var pendingIntent = PendingIntent.GetService(context, 0, intent, PendingIntentFlags.UpdateCurrent);
            var alarmManager = context.GetSystemService(Context.AlarmService) as AlarmManager;
            alarmManager?.Cancel(pendingIntent);
        }
    }
}