using Android.Content;
using JetBrains.Annotations;

namespace Droid.Infrastructure.Alarm
{
    public interface IRssAlarmManager
    {
        void InitAlarm<T>([NotNull] Context context, int interval);

        void RemoveAlarm<T>([NotNull] Context context);
    }
}