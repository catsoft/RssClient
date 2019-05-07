using System;
using System.Reactive.Linq;
using Android.OS;
using Android.Views;
using Android.Widget;
using Core.Extensions;
using Core.ViewModels.Settings.Pushes;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;

namespace Droid.Screens.Settings.Pushes
{
    public class SettingsPushesFragment : BaseFragment<SettingsPushesViewModel>
    {
        [NotNull] private SettingsPushesFragmentViewHolder _viewHolder;
        
        protected override int LayoutId => Resource.Layout.fragment_settings_pushes;

        public override bool IsRoot => false;

        // ReSharper disable once NotNullMemberIsNotInitialized
        // ReSharper disable once EmptyConstructor
        public SettingsPushesFragment() { }
        
        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState).NotNull();

            _viewHolder = new SettingsPushesFragmentViewHolder(view);

            OnActivation(disposable =>
            {
                _viewHolder.CheckBox.Events()
                    .NotNull()
                    .CheckedChange
                    .NotNull()
                    .Select(w => w.NotNull().IsChecked)
                    .InvokeCommand(ViewModel.UpdateIsShowPushesCommand)
                    .AddTo(disposable);
                
                ViewModel.AppConfigurationViewModel.WhenAnyValue(w => w.AppConfiguration)
                    .NotNull() 
                    .Select(w => w.NotNull().IsShowPush)
                    .Subscribe(w => _viewHolder.CheckBox.Checked = w)
                    .AddTo(disposable);
            });
            
            return view;
        }
    }
}
