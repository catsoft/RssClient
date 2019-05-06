using System;
using System.Reactive.Disposables;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Autofac;
using Core;
using Core.Extensions;
using Core.ViewModels.RssFeeds.RssFeedsUpdater;

namespace Droid.Services.RssFeedUpdate
{
    [Service(Permission = Manifest.Permission.ForegroundService)]
    public class RssFeedUpdateService : Service
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            App.BuildIfNever();

            InitNotify();
            
            var viewModel = App.Container.Resolve<RssFeedsUpdaterViewModel>();

            viewModel.HardUpdateCommand.ExecuteIfCan().AddTo(_disposables);

            viewModel.HardUpdateCommand.Subscribe(w => { StopSelf(); }).AddTo(_disposables);
            
            return base.OnStartCommand(intent, flags, startId);
        }

        private void InitNotify()
        {
            var channelId = "rssList";
            var builder = new NotificationCompat.Builder(this, channelId);
            builder.SetContentTitle("Rss list");
            builder.SetContentText("Updating list");
            builder.SetAutoCancel(false);

            var notification = builder.Build();
            
            StartForeground(1, notification);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            
            _disposables.Dispose();
        }

//        public const int Min15 = 1000 * 60 * 15;
        public const int Min15 = 1000 * 5;
        
        public static void InitAlarm(Context context)
        {
            var intent = new Intent(context, typeof(RssFeedUpdateService));
            var pendingIntent = PendingIntent.GetService(context, 0, intent, PendingIntentFlags.UpdateCurrent);
            var alarmManager = context.GetSystemService(AlarmService) as AlarmManager;
            alarmManager?.SetRepeating(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + Min15,Min15, pendingIntent);
        }

        public static void RemoveAlarm(Context context)
        {
            var intent = new Intent(context, typeof(RssFeedUpdateService));
            var pendingIntent = PendingIntent.GetService(context, 0, intent, PendingIntentFlags.UpdateCurrent);
            var alarmManager = context.GetSystemService(AlarmService) as AlarmManager;
            alarmManager?.SetRepeating(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + Min15,Min15, pendingIntent);
        }
    }
}