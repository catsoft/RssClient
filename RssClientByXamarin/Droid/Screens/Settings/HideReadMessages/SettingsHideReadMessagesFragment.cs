using System;
using System.Reactive.Linq;
using Android.OS;
using Android.Views;
using Android.Widget;
using Core.Extensions;
using Core.ViewModels.Settings.HideReadMessages;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;

namespace Droid.Screens.Settings.HideReadMessages
{
    public class SettingsHideReadMessagesFragment : BaseFragment<SettingsHideReadMessagesViewModel>
    {
        [NotNull] private SettingsHideReadMessagesFragmentViewHolder _viewHolder;
        
        protected override int LayoutId => Resource.Layout.fragment_settings_read_messages;

        public override bool IsRoot => false;

        protected override void RestoreState(Bundle saved) { }

        // ReSharper disable once EmptyConstructor
        // ReSharper disable once NotNullMemberIsNotInitialized
        public SettingsHideReadMessagesFragment()
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState).NotNull();
            
            _viewHolder = new SettingsHideReadMessagesFragmentViewHolder(view);

            OnActivation(disposable =>
            {
                _viewHolder.CheckBox.Events().CheckedChange
                    .Select(w => w.IsChecked)
                    .InvokeCommand(ViewModel.UpdateHideReadMessagesCommand)
                    .AddTo(disposable);
                
                ViewModel.AppConfigurationViewModel.WhenAnyValue(w => w.AppConfiguration)
                    .Select(w => w.HideReadMessages)
                    .Subscribe(w => _viewHolder.CheckBox.Checked = w)
                    .AddTo(disposable);
            });
            
            return view;
        }
    }
}
