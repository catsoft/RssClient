using System;
using System.Reactive.Linq;
using Android.OS;
using Android.Views;
using Android.Widget;
using Core.Configuration.Settings;
using Core.Extensions;
using Core.ViewModels.Settings.Theme;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;

namespace Droid.Screens.Settings.Theme
{
    public class SettingsThemeFragment : BaseFragment<SettingsThemeViewModel>
    {
        // ReSharper disable once NotNullMemberIsNotInitialized
        [NotNull] private SettingsThemeFragmentViewHolder _viewHolder;
        
        protected override int LayoutId => Resource.Layout.fragment_settings_theme;

        public override bool IsRoot => false;

        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState).NotNull();

            _viewHolder = new SettingsThemeFragmentViewHolder(view);

            OnActivation(disposable =>
            {
                _viewHolder.RadioGroup.Events().CheckedChange
                    .Select(w => w.CheckedId)
                    .Select(ConvertToTheme)
                    .InvokeCommand(ViewModel.UpdateAppThemeCommand)
                    .AddTo(disposable);
                
                ViewModel.WhenAnyValue(w => w.AppConfigurationViewModel.AppConfiguration)
                    .Select(w => w.AppTheme)
                    .Select(ConvertToId)
                    .Subscribe(w => _viewHolder.RadioGroup.Check(w))
                    .AddTo(disposable);

                ViewModel.UpdateAppThemeCommand
                    .Subscribe(w => Activity.Recreate())
                    .AddTo(disposable);
            });
    
            return view;
        }

        private int ConvertToId(AppTheme appTheme)
        {
            switch (appTheme)
            {
                default:
                    return _viewHolder.DefaultRadioButton.Id;
                case AppTheme.Light:
                    return _viewHolder.LightRadioButton.Id;
                case AppTheme.Dark:
                    return _viewHolder.DarkRadioButton.Id;
            }
        }

        private AppTheme ConvertToTheme(int id)
        {
            if (id == _viewHolder.DarkRadioButton.Id) return AppTheme.Dark;

            if (id == _viewHolder.LightRadioButton.Id) return AppTheme.Light;

            if (id == _viewHolder.DefaultRadioButton.Id) return AppTheme.Default;

            return AppTheme.Default;
        }
    }
}
