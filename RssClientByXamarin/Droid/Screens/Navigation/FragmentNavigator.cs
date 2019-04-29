using System.Linq;
using Android.Support.Transitions;
using Android.Support.V4.App;
using Android.Views;
using Core.Configuration.Settings;
using Core.Extensions;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.Navigation
{
    public class FragmentNavigator
    {
        [NotNull] private readonly FragmentManager _fragmentManager;
        [NotNull] private readonly ViewGroup _container;

        public FragmentNavigator([NotNull] FragmentManager fragmentManager,
            [NotNull] AppConfiguration appConfiguration,
            [NotNull] ViewGroup container)
        {
            _fragmentManager = fragmentManager;
            AppConfiguration = appConfiguration;
            _container = container;
        }

        [NotNull] public AppConfiguration AppConfiguration { get; set; }

        public void SetRootFragment([NotNull] Fragment fragment)
        {
            _fragmentManager.NotNull()
                .BeginTransaction()
                .NotNull()
                .Add(_container.Id, fragment)
                .NotNull()
                .Commit();
        }
        
        public void GoTo([NotNull] Fragment fragment)
        {
            var previousFragment = _fragmentManager.Fragments?.LastOrDefault();

            SetAnimation(fragment, previousFragment);

            _fragmentManager.NotNull()
                .BeginTransaction()
                .NotNull()
                .Replace(_container.Id, fragment)
                .NotNull()
                .AddToBackStack(null)
                .NotNull()
                .Commit();
        }

        public void GoBack()
        {
            _fragmentManager.PopBackStack();
        }

        private void SetAnimation([NotNull] Fragment fragment,
            [CanBeNull] Fragment previousFragment
        )
        {
            CommonAnimate(fragment,
                previousFragment,
                AppConfiguration.GetCalculationAnimationTime(),
                AppConfiguration.AnimationEnter.ToVisibility(),
                AppConfiguration.AnimationExit.ToVisibility(),
                AppConfiguration.AnimationEnter.ToVisibility(),
                AppConfiguration.AnimationExit.ToVisibility());
        }

        private void CommonAnimate(
            [NotNull] Fragment fragment,
            [CanBeNull] Fragment previousFragment,
            int duration,
            [CanBeNull] Visibility enterEnterFragment,
            [CanBeNull] Visibility exitEnterFragment,
            [CanBeNull] Visibility enterPopFragment,
            [CanBeNull] Visibility exitPopFragment)
        {
            if (previousFragment != null) 
                SetRightFragmentAnimation(previousFragment, enterPopFragment, exitPopFragment, duration);

            SetRightFragmentAnimation(fragment, enterEnterFragment, exitEnterFragment, duration);

            var listener = new TransitionListener(() => SetReverseFragmentAnimation(fragment, enterPopFragment, exitPopFragment, duration));

            enterEnterFragment?.AddListener(listener);
            exitEnterFragment?.AddListener(listener);
        }

        private void SetRightFragmentAnimation([NotNull] Fragment fragment,
            [CanBeNull] Visibility enterVisibility,
            [CanBeNull] Visibility exitVisibility,
            long duration)
        {
            if (enterVisibility != null)
            {
                enterVisibility.SetDuration(duration);
                if (AppConfiguration.IsDelay)
                    enterVisibility.SetStartDelay(duration);
                fragment.EnterTransition = enterVisibility;
            }
            else
            {
                fragment.EnterTransition = null;
            }

            if (exitVisibility != null)
            {
                exitVisibility.SetDuration(duration);
                if (AppConfiguration.IsDelay)
                    exitVisibility.SetStartDelay(0);
                fragment.ExitTransition = exitVisibility;
            }
            else
            {
                fragment.ExitTransition = null;
            }
        }

        private void SetReverseFragmentAnimation([NotNull] Fragment fragment,
            [CanBeNull] Visibility enterVisibility,
            [CanBeNull] Visibility exitVisibility,
            long duration)
        {
            if (enterVisibility != null)
            {
                enterVisibility.SetDuration(duration);
                if (AppConfiguration.IsDelay)
                    enterVisibility.SetStartDelay(0);
                fragment.EnterTransition = enterVisibility;
            }
            else
            {
                fragment.EnterTransition = null;
            }

            if (exitVisibility != null)
            {
                exitVisibility.SetDuration(duration);
                if (AppConfiguration.IsDelay)
                    exitVisibility.SetStartDelay(enterVisibility != null ? duration : 0);
                fragment.ExitTransition = exitVisibility;
            }
            else
            {
                fragment.ExitTransition = null;
            }
        }
    }
}