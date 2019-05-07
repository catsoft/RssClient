using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Autofac;
using Core;
using Core.Analytics;
using Core.Configuration.Settings;
using Core.Extensions;
using Core.Repositories.Configurations;
using Droid.Configurations.Canals;
using Droid.Container.Modules;
using Droid.Infrastructure.Alarm;
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

            var configurationRepository = App.Container.Resolve<IConfigurationRepository>();
            var appConfiguration = configurationRepository.GetSettings<AppConfiguration>();

            BuildCanals(configurationRepository);
            
            InitAlarms(appConfiguration);

            var log = App.Container.Resolve<ILog>().NotNull();
            
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

        private void InitAlarms(AppConfiguration appConfiguration)
        {
            var alarmManager = App.Container.Resolve<IRssAlarmManager>();
            alarmManager.InitAlarm<RssFeedUpdateService>(this, appConfiguration.AutoUpdateInterval);
        }
        
        private void BuildCanals(IConfigurationRepository configurationRepository)
        {
            var settings = configurationRepository.GetSettings<RssListCanal>();
            settings = RssListCanal.DefaultInit(settings);
            
            CreateCanalIfNeed(settings);
            
            configurationRepository.SaveSetting(settings);
        }
        
        private void CreateCanalIfNeed(RssListCanal rssListCanal)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var canal = new NotificationChannel(rssListCanal.Id, rssListCanal.Name, NotificationImportance.Default);
                canal.Description = rssListCanal.Description;

                var manager = GetSystemService(NotificationService) as NotificationManager;
                manager?.CreateNotificationChannel(canal);
            }
        }
    }
}
