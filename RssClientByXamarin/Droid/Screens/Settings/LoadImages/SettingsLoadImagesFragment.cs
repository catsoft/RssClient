using System;
using System.Reactive.Linq;
using Android.OS;
using Android.Views;
using Android.Widget;
using Core.Extensions;
using Core.ViewModels.Settings.LoadImages;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;

namespace Droid.Screens.Settings.LoadImages
{
    public class SettingsLoadImagesFragment : BaseFragment<SettingsLoadImagesViewModel>
    {
        // ReSharper disable once NotNullMemberIsNotInitialized
        [NotNull] private SettingsLoadImagesFragmentViewHolder _viewHolder;
        
        protected override int LayoutId => Resource.Layout.fragment_settings_load_images;

        public override bool IsRoot => false;

        // ReSharper disable once NotNullMemberIsNotInitialized
        // ReSharper disable once EmptyConstructor
        public SettingsLoadImagesFragment() { }
        
        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState).NotNull();

            _viewHolder = new SettingsLoadImagesFragmentViewHolder(view);

            OnActivation(disposable =>
            {
                _viewHolder.CheckBox.Events().CheckedChange
                    .Select(w => w.IsChecked)
                    .InvokeCommand(ViewModel.UpdateLoadAndShowImagesCommand)
                    .AddTo(disposable);
                
                ViewModel.AppConfigurationViewModel.WhenAnyValue(w => w.AppConfiguration)
                    .Select(w => w.LoadAndShowImages)
                    .Subscribe(w => _viewHolder.CheckBox.Checked = w)
                    .AddTo(disposable);
            });
            
            return view;
        }
    }
}
