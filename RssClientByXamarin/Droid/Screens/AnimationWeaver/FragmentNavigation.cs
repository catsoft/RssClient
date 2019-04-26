using System;
using System.Linq;
using Android.Support.Transitions;
using Android.Support.V4.App;
using Android.Views;
using Core.Configuration.Settings;
using Droid.NativeExtension;

namespace Droid.Screens.AnimationWeaver
{
    public class FragmentNavigation
    {
        public AppConfiguration AppConfiguration { get; set; }
        
        private readonly FragmentActivity _fragmentActivity;
        private readonly ViewGroup _container;

        public FragmentNavigation(FragmentActivity fragmentActivity, AppConfiguration appConfiguration, ViewGroup container)
        {
            _fragmentActivity = fragmentActivity;
            AppConfiguration = appConfiguration;
            _container = container;
        }

        public void GoTo(Fragment fragment)
        {
            var previousFragment = _fragmentActivity.SupportFragmentManager.Fragments?.LastOrDefault();

            SetAnimation(fragment, previousFragment);

            _fragmentActivity.SupportFragmentManager.BeginTransaction()
                .Replace(_container.Id, fragment)
                .AddToBackStack(null)
                .Commit();
        }

        private void SetAnimation(Fragment fragment,
            Fragment previousFragment
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
            Fragment fragment,
            Fragment previousFragment,
            int duration,
            Visibility enterEnterFragment,
            Visibility exitEnterFragment,
            Visibility enterPopFragment,
            Visibility exitPopFragment)
        {
            if (previousFragment != null)
            {
                SetRightFragmentAnimation(previousFragment, enterPopFragment, exitPopFragment, duration);
            }

            SetRightFragmentAnimation(fragment, enterEnterFragment, exitEnterFragment, duration);
            
            if (enterEnterFragment != null)
            {
                var listener = new TransitionListener(() =>
                {
                    SetReverseFragmentAnimation(fragment, enterPopFragment, exitPopFragment, duration);
                });

                enterEnterFragment.AddListener(listener);
                exitEnterFragment.AddListener(listener);
            }
        }

        private void SetRightFragmentAnimation(Fragment fragment,
            Visibility enterVisibility,
            Visibility exitVisibility,
            long duration)
        {
            if (enterVisibility != null)
            {
                enterVisibility.SetDuration(duration);
                if(AppConfiguration.IsDelay)
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
                if(AppConfiguration.IsDelay)
                    exitVisibility.SetStartDelay(0);
                fragment.ExitTransition = exitVisibility;
            }
            else
            {
                fragment.ExitTransition = null;
            }
        }
        
        private void SetReverseFragmentAnimation(Fragment fragment,
            Visibility enterVisibility,
            Visibility exitVisibility,
            long duration)
        {
            if (enterVisibility != null)
            {
                enterVisibility.SetDuration(duration);
                if(AppConfiguration.IsDelay)
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
                if(AppConfiguration.IsDelay)
                    exitVisibility.SetStartDelay(enterVisibility != null ? duration : 0);
                fragment.ExitTransition = exitVisibility;
            }
            else
            {
                fragment.ExitTransition = null;
            }
        }
    }

    public class TransitionListener : TransitionListenerAdapter
    {
        private readonly Action _action;

        public TransitionListener(Action action)
        {
            _action = action;
        }

        public override void OnTransitionEnd(Transition transition)
        {
            base.OnTransitionEnd(transition);

            _action?.Invoke();
            
            transition.RemoveListener(this);
        }
    }
}