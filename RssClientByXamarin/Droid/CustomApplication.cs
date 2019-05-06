using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Autofac;
using Core;
using Core.Analytics;
using Core.Extensions;
using Droid.Container.Modules;
using Droid.Services.RssFeedUpdate;

namespace Droid
{
    [Application]
    public class CustomApplication : Application
    {
        private const string KeyDebugAndroid = "3a26d323-c850-4b9d-b9bd-f4402dcd9995";

        // ReSharper disable once UnusedMember.Local
        private const string KeyBattleAndroid = "d6157339-63f5-4c0c-9df1-ffe26b8f851c";

        public CustomApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }

        public override void OnCreate()
        {
            base.OnCreate();

            App.BuildIfNever(new PlatformModule());

            var log = App.Container.Resolve<ILog>().NotNull();

            BuildAlerts();

#if DEBUG
            {
                log.SetApiKey(KeyDebugAndroid);
            }
#else
{
                log.SetApiKey(KeyBattleAndroid);
}
#endif
        }

        private void BuildAlerts()
        {
            var intent = new Intent(this, typeof(RssFeedUpdateService));
            var pendingIntent = PendingIntent.GetService(this, 0, intent, PendingIntentFlags.UpdateCurrent);
            var alarmManager = GetSystemService(AlarmService) as AlarmManager;
            alarmManager?.SetRepeating(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + 10000,10000, pendingIntent);
        }
    }
}
