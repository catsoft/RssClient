using Android.OS;
using Android.Views;
using Core.Extensions;
using Core.ViewModels.Settings.Animations;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;

namespace Droid.Screens.Settings.Animations
{
    public class SettingsAnimationFragment : BaseFragment<SettingsAnimationViewModel>
    {
        [NotNull] private SettingsAnimationFragmentViewHolder _viewHolder;
        
        protected override int LayoutId => Resource.Layout.fragment_settings_animation;

        public override bool IsRoot => false;

        // ReSharper disable once NotNullMemberIsNotInitialized
        // ReSharper disable once EmptyConstructor
        public SettingsAnimationFragment()
        {
        }

        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState).NotNull();

            _viewHolder = new SettingsAnimationFragmentViewHolder(view);
            
            OnActivation((disposables) => _viewHolder.GoToCustomizeButton.Events()
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
