using System;
using System.Reactive.Linq;
using Android.OS;
using Android.Views;
using Android.Widget;
using Core.Extensions;
using Core.ViewModels.Settings.AutoUpdating;
using Droid.Screens.Navigation;
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
                    .Select(w => w.AutoUpdateInterval)
                    .Subscribe(w => _viewHolder.EditTextInterval.Text = (w / 1000 / 60).ToString())
                    .AddTo(disposable);
                
                _viewHolder.EditTextInterval.Events()
                    .TextChanged
                    .NotNull()
                    .Select(w => _viewHolder.EditTextInterval.Text)
                    .Select(int.Parse)
                    .InvokeCommand(ViewModel.UpdateAutoUpdateIntervalCommand)
                    .AddTo(disposable);
            });
            
            return view;
        }
    }
}