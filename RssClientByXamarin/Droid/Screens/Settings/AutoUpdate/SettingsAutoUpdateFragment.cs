using System;
using System.Reactive.Linq;
using Android.OS;
using Android.Views;
using Android.Widget;
using Autofac;
using Core;
using Core.Extensions;
using Core.ViewModels.Settings.AutoUpdating;
using Droid.Infrastructure.Alarm;
using Droid.Screens.Navigation;
using Droid.Services.RssFeedUpdate;
using JetBrains.Annotations;
using ReactiveUI;

namespace Droid.Screens.Settings.AutoUpdate
{
    public class SettingsAutoUpdateFragment : BaseFragment<SettingsAutoUpdateViewModel>
    {
        [NotNull] private SettingsAutoUpdateFragmentViewHolder _viewHolder;

        protected override int LayoutId => Resource.Layout.fragment_settings_auto_update;
        
        public override bool IsRoot => false;
        
        // ReSharper disable once NotNullMemberIsNotInitialized
        // ReSharper disable once EmptyConstructor
        public SettingsAutoUpdateFragment() { }
        
        protected override void RestoreState(Bundle saved) { }
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState).NotNull();

            _viewHolder = new SettingsAutoUpdateFragmentViewHolder(view);

            OnActivation(disposable =>
            {
                this.Bind(ViewModel, model => model.Interval, fragment => fragment._viewHolder.EditTextInterval.Text)
                    .AddTo(disposable);
                
                _viewHolder.CheckBox.Events()
                    .CheckedChange
                    .NotNull()
                    .Select(w => w.NotNull().IsChecked)
                    .InvokeCommand(ViewModel.UpdateIsAutoUpdateCommand)
                    .AddTo(disposable);
                
                ViewModel.AppConfigurationViewModel.WhenAnyValue(w => w.AppConfiguration)
                    .NotNull() 
                    .Select(w => w.NotNull().IsAutoUpdate)
                    .Subscribe(w => _viewHolder.CheckBox.Checked = w)
                    .AddTo(disposable);
                
                ViewModel.AppConfigurationViewModel.WhenAnyValue(w => w.AppConfiguration)
                    .NotNull()
                    .Select(w => w.NotNull().AutoUpdateInterval)
                    .Subscribe(w =>
                    {
                        var alarmManager = App.Container.Resolve<IRssAlarmManager>().NotNull();
                        alarmManager.RemoveAlarm<RssFeedUpdateService>(Activity);
                        alarmManager.InitAlarm<RssFeedUpdateService>(Activity, w);
                    })
                    .AddTo(disposable);
                         
                ViewModel.AppConfigurationViewModel.WhenAnyValue(w => w.AppConfiguration)
                    .NotNull()
                    .Subscribe(w =>
                    {
                        var alarmManager = App.Container.Resolve<IRssAlarmManager>().NotNull();
                        if (w.NotNull().IsAutoUpdate)
                            alarmManager.InitAlarm<RssFeedUpdateService>(Activity, w.AutoUpdateInterval);
                        else
                            alarmManager.RemoveAlarm<RssFeedUpdateService>(Activity);
                    })
                    .AddTo(disposable);
            });
            
            return view;
        }
    }
}