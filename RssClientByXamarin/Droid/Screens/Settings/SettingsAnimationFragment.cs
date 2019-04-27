using Android.OS;
using Android.Views;
using Core.Extensions;
using Core.ViewModels.Settings;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;

namespace Droid.Screens.Settings
{
    public class SettingsAnimationFragment : BaseFragment<SettingsAnimationViewModel>
    {
        // ReSharper disable once NotNullMemberIsNotInitialized
        [NotNull] private SettingsAnimationFragmentViewModel _viewModel;
        
        protected override int LayoutId => Resource.Layout.fragment_settings_animation;

        public override bool IsRoot => false;

        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState).NotNull();

            _viewModel = new SettingsAnimationFragmentViewModel(view);
            
            OnActivation((disposables) => _viewModel.GoToCustomizeButton.Events()
                .NotNull()
                .Click
                .NotNull()
                .SelectUnit()
                .InvokeCommand(ViewModel.GoToWeaverCommand)
                .AddTo(disposables));
            
            return view;
        }
    }
}
