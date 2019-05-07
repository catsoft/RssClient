using System;
using System.Reactive.Disposables;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Autofac;
using Core;
using Core.Configuration.Settings;
using Core.Extensions;
using Core.Repositories.Configurations;
using Core.ViewModels.RssFeeds.RssFeedsUpdater;
using Droid.Configurations.Canals;
using Droid.Container.Modules;

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
            App.BuildIfNever(new PlatformModule());

            var configurationRepository = App.Container.Resolve<IConfigurationRepository>();

            var rssListCanal = configurationRepository.GetSettings<RssListCanal>();

            var appConfiguration = configurationRepository.GetSettings<AppConfiguration>();

            if (appConfiguration.IsShowPush)
            {
                ShowForegroundNotification(rssListCanal);
            }

            var viewModel = App.Container.Resolve<RssFeedsUpdaterViewModel>();

            viewModel.HardUpdateCommand.ExecuteIfCan().AddTo(_disposables);

            viewModel.UpdateCommand.Subscribe(w =>
            {
                StopForeground(StopForegroundFlags.Remove);
                StopSelf();
            }).AddTo(_disposables);

            return base.OnStartCommand(intent, flags, startId);
        }

        private void ShowForegroundNotification(RssListCanal rssListCanal)
        {
            var notification = rssListCanal.GenerateNotification(this);

            StartForeground(1, notification);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            
            _disposables.Dispose();
        }
    }
}