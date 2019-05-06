using System;
using System.Reactive.Disposables;
using Android.App;
using Android.Content;
using Android.OS;
using Autofac;
using Core;
using Core.Extensions;
using Core.ViewModels.RssFeeds.RssFeedsUpdater;

namespace Droid.Services.RssFeedUpdate
{
    [Service]
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

            var viewModel = App.Container.Resolve<RssFeedsUpdaterViewModel>();

            viewModel.HardUpdateCommand.ExecuteIfCan().AddTo(_disposables);

            viewModel.HardUpdateCommand.Subscribe(w => { StopSelf(); }).AddTo(_disposables);
            
            return base.OnStartCommand(intent, flags, startId);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            
            _disposables.Dispose();
        }
    }
}