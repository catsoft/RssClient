using System;
using System.Linq;
using Android.OS;
using Android.Support.Transitions;
using Android.Support.V4.App;
using Droid.Container;
using Droid.Repository;
using Shared.Configuration;

namespace Droid.Screens.Navigation
{
    public abstract class FragmentActivity : BurgerActivity
    {
        [Inject]
        private IConfigurationRepository _configurationRepository;
        
        protected abstract int? ContainerId { get; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SupportFragmentManager.BackStackChanged += (sender, args) =>
            {
                var lastFragment = SupportFragmentManager.Fragments.LastOrDefault();
                if (lastFragment is TitleFragment titleFragment)
                {
                    Title = titleFragment.Title;
                }

                UpdateDrawerState();
            };
        }

        public void AddFragment(Fragment fragment, CacheState cacheState = CacheState.New)
        {
            DoOrNo(transaction =>
            {
                var previousFragment = SupportFragmentManager.FindFragmentById(ContainerId.Value);
                SetCustomAnimation(previousFragment, fragment);
                
//                transaction.SetCustomAnimations(Resource.Animation.enter_from_right, Resource.Animation.exit_to_left,
//                    Resource.Animation.enter_from_left, Resource.Animation.exit_to_right);
                var type = fragment.GetType();

                switch (cacheState)
                {
                    case CacheState.New:
                        transaction.Replace(ContainerId ?? 0, fragment);
                        transaction.AddToBackStack(fragment.GetType().FullName);
                        break;
                    case CacheState.Old:
                        var old = SupportFragmentManager.Fragments.LastOrDefault(w => w.GetType() == type);
                        if (old == null)
                        {
                            transaction.Replace(ContainerId ?? 0, fragment);
                            transaction.AddToBackStack(fragment.GetType().FullName);
                        }
                        else
                            transaction.Show(old);

                        break;
                    case CacheState.Replace:
                        var oldReplace = SupportFragmentManager.Fragments.LastOrDefault(w => w.GetType() == type);
                        if (oldReplace != null)
                        {
                            DoOrNo(fragmentTransaction => transaction.Remove(oldReplace));
                        }

                        transaction.Replace(ContainerId ?? 0, fragment);
                        transaction.AddToBackStack(fragment.GetType().FullName);
                        break;
                }
            });
        }

        private void SetCustomAnimation(Fragment previousFragment, Fragment fragment)
        {
            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();

            var time = appConfiguration.GetCalculationAnimationTime();
            var animationType = appConfiguration.AnimationType;

            switch (animationType)
            {
                case AnimationType.None:
                    AnimateNone(previousFragment, fragment, time);
                    break;
                case AnimationType.OnlyFade:
                    AnimateOnlyFade(previousFragment, fragment, time);
                    break;
                case AnimationType.ExitToBottomEnterFromBottom:
                    AnimateOnlySlide(previousFragment, fragment, time);
                    break;
                case AnimationType.ExitToBottomEnterFade:
                    AnimateExitToBottomEnterFade(previousFragment, fragment, time);
                    break;
                case AnimationType.ExitFadeEnterFromBottom:
                    AnimateExitFadeEnterFromBottom(previousFragment, fragment, time);
                    break;
            }
        }

        private void AnimateNone(Fragment previousFragment, Fragment fragment, int time)
        {
        }

        private void AnimateOnlyFade(Fragment previousFragment, Fragment fragment, int time)
        {
            if (previousFragment != null)
            {
                var exitTransition = new Fade();
                exitTransition.Mode = Fade.ModeOut;
                exitTransition.SetDuration(time);
                previousFragment.ExitTransition = exitTransition;
                fragment.ExitTransition = exitTransition;
            }
            
            var enterTransition = new Fade();
            enterTransition.Mode = Fade.ModeOut;
            enterTransition.SetDuration(time);
            fragment.EnterTransition = enterTransition;
        }

        private void AnimateOnlySlide(Fragment previousFragment, Fragment fragment, int time)
        {
            if (previousFragment != null)
            {
                var exitTransition = new Slide();
                exitTransition.Mode = Slide.ModeOut;
                exitTransition.SetDuration(time);
                previousFragment.ExitTransition = exitTransition;
                
                var enter = new Slide();
                enter.Mode = Slide.ModeOut;
                enter.SetDuration(time);
                enter.SetStartDelay(time);
                previousFragment.EnterTransition = enter;
            }
            
            var enterTransition = new Slide();
            enterTransition.Mode = Slide.ModeIn;
            enterTransition.SetStartDelay(time);
            enterTransition.SetDuration(time);
            fragment.EnterTransition = enterTransition;
            
            
            var exitTransition2 = new Slide();
            exitTransition2.Mode = Slide.ModeIn;
            exitTransition2.SetDuration(time);
            fragment.ExitTransition = exitTransition2;
        }

        private void AnimateExitToBottomEnterFade(Fragment previousFragment, Fragment fragment, int time)
        {
            if (previousFragment != null)
            {
                var exitTransition = new Slide();
                exitTransition.Mode = Slide.ModeOut;
                exitTransition.SetDuration(time);
                previousFragment.ExitTransition = exitTransition;
            }
            
            var enterTransition = new Slide();
            enterTransition.Mode = Slide.ModeOut;
            enterTransition.SetStartDelay(time);
            enterTransition.SetDuration(time);
            fragment.EnterTransition = enterTransition;
        }

        private void AnimateExitFadeEnterFromBottom(Fragment previousFragment, Fragment fragment, int time)
        {
            if (previousFragment != null)
            {
                var exitTransition = new Fade();
                exitTransition.Mode = Fade.ModeOut;
                exitTransition.SetDuration(time);
                previousFragment.ExitTransition = exitTransition;
            }
            
            var enterTransition = new Fade();
            enterTransition.Mode = Fade.ModeOut;
            enterTransition.SetStartDelay(time);
            enterTransition.SetDuration(time);
            fragment.EnterTransition = enterTransition;
        }

        public void RemoveFragment(Fragment fragment)
        {
            DoOrNo(transaction =>
            {
                transaction.Remove(fragment);

                transaction.AddToBackStack(fragment.GetType().Name);
            });
        }

        private void DoOrNo(Action<FragmentTransaction> doOrNow)
        {
            if (ContainerId.HasValue)
            {
                using (var transaction = SupportFragmentManager.BeginTransaction())
                {
                    doOrNow?.Invoke(transaction);

                    transaction.Commit();
                }
            }
        }

        public override void OnBackPressed()
        {
            if (IsHomeToggle)
                Finish();
            else
                base.OnBackPressed();
        }
    }

    public enum CacheState
    {
        /// <summary>
        /// Добавляет новый фрагмент
        /// </summary>
        New,

        /// <summary>
        /// Если есть в стеке, показывает его, если нет, создает новый
        /// </summary>
        Old,

        /// <summary>
        /// Если есть старый, удаляет, помещает в стек новый
        /// </summary>
        Replace,
    }
}