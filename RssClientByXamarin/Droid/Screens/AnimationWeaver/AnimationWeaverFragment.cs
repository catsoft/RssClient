using System;
using System.Reactive.Linq;
using Android.OS;
using Android.Views;
using Android.Widget;
using Core.Extensions;
using Core.ViewModels.AnimationWeaver;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;

namespace Droid.Screens.AnimationWeaver
{
    public class AnimationWeaverFragment : BaseFragment<AnimationWeaverViewModel>
    {
        // ReSharper disable once NotNullMemberIsNotInitialized
        [NotNull] private FragmentNavigation _fragmentNavigation;
        
        // ReSharper disable once NotNullMemberIsNotInitialized
        [NotNull] private AnimationWeaverFragmentViewHolder _viewHolder;

        // ReSharper disable once EmptyConstructor
        // ReSharper disable once NotNullMemberIsNotInitialized
        public AnimationWeaverFragment()
        {
        }
        
        protected override int LayoutId => Resource.Layout.fragment_animation_weaver;
        public override bool IsRoot => false;

        protected override void RestoreState(Bundle saved)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState).NotNull();

            _viewHolder = new AnimationWeaverFragmentViewHolder(view);

            _fragmentNavigation = new FragmentNavigation(Activity, ViewModel.AppConfiguration, _viewHolder.Container);

            Title = ViewModel.Title;

            OnActivation(disposables =>
            {
                var enterSpinnerAdapter =
                    new ArrayAdapter<string>(Activity, Resource.Layout.support_simple_spinner_dropdown_item, ViewModel.TypesLocalized);
                _viewHolder.EnterSpinner.Adapter = enterSpinnerAdapter;

                ViewModel.WhenAnyValue(w => w.EnterAnim)
                    .NotNull()
                    .Subscribe(w => _viewHolder.EnterSpinner.SetSelection(ViewModel.Types.IndexOf(w)))
                    .AddTo(disposables);

                _viewHolder.EnterSpinner.Events()
                    .NotNull()
                    .ItemSelected
                    .NotNull()
                    .Select(w => ViewModel.Types[w.NotNull().Position])
                    .InvokeCommand(ViewModel.UpdateEnterAnimCommand)
                    .AddTo(disposables);


                var exitSpinnerAdapter =
                    new ArrayAdapter<string>(Activity, Resource.Layout.support_simple_spinner_dropdown_item, ViewModel.TypesLocalized);
                _viewHolder.ExitSpinner.Adapter = exitSpinnerAdapter;

                ViewModel.WhenAnyValue(w => w.ExitAnim)
                    .NotNull()
                    .Subscribe(w => _viewHolder.ExitSpinner.SetSelection(ViewModel.Types.IndexOf(w)))
                    .AddTo(disposables);

                _viewHolder.ExitSpinner.Events()
                    .NotNull()
                    .ItemSelected
                    .NotNull()
                    .Select(w => ViewModel.Types[w.NotNull().Position])
                    .InvokeCommand(ViewModel.UpdateExitAnimCommand)
                    .AddTo(disposables);


                var speedsAdapter =
                    new ArrayAdapter<string>(Activity, Resource.Layout.support_simple_spinner_dropdown_item, ViewModel.SpeedLocalized);
                _viewHolder.SpeedSpinner.Adapter = speedsAdapter;

                ViewModel.WhenAnyValue(w => w.AnimationSpeed)
                    .NotNull()
                    .Subscribe(w => _viewHolder.SpeedSpinner.SetSelection(ViewModel.Speeds.IndexOf(w)))
                    .AddTo(disposables);

                _viewHolder.SpeedSpinner.Events()
                    .NotNull()
                    .ItemSelected
                    .NotNull()
                    .Select(w => ViewModel.Speeds[w.NotNull().Position])
                    .InvokeCommand(ViewModel.UpdateSpeedAnimCommand)
                    .AddTo(disposables);


                ViewModel.WhenAnyValue(w => w.IsDelay)
                    .NotNull()
                    .Subscribe(SetIsDelay)
                    .AddTo(disposables);

                _viewHolder.RadioGroup.Events()
                    .NotNull()
                    .CheckedChange
                    .NotNull()
                    .Select(w => w.NotNull().CheckedId == _viewHolder.RadioButtonDelay.Id)
                    .InvokeCommand(ViewModel.UpdateIsDelayAnimCommand)
                    .AddTo(disposables);

                ViewModel.AppConfigurationObservable
                    .Subscribe(w => _fragmentNavigation.AppConfiguration = w.NotNull())
                    .AddTo(disposables);

                _fragmentNavigation.GoTo(new AnimatableFragment(_fragmentNavigation));
            });

            return view;
        }

        private void SetIsDelay(bool isDelay)
        {
            if (_fragmentNavigation.AppConfiguration.IsDelay)
                _viewHolder.RadioButtonDelay.Checked = true;
            else
                _viewHolder.RadioButtonNotDelay.Checked = true;
        }
    }
}