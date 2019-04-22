using System;
using System.Linq;
using Android.OS;
using Android.Support.Transitions;
using Android.Support.V4.App;
using Android.Views;
using Autofac;
using Droid.Container;
using Droid.Repositories.Configuration;
using Shared;
using Shared.Configuration.Settings;
using Shared.Infrastructure.ViewModels;

namespace Droid.Screens.Navigation
{
    public abstract class FragmentActivity<TViewModel> : BurgerActivity<TViewModel>, IFragmentManager
        where TViewModel : ViewModel
    {
        [Inject] private IConfigurationRepository _configurationRepository;

        protected abstract int? ContainerId { get; }

        public void AddFragment(Fragment fragment, CacheState cacheState = CacheState.New)
        {
            DoOrNo(transaction =>
            {
                if (!ContainerId.HasValue) return;

                var previousFragment = SupportFragmentManager.FindFragmentById(ContainerId.Value);
                SetAnimation(previousFragment, fragment);

                var type = fragment.GetType();
                //TODO подумать и убрать?
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
                        {
                            transaction.Show(old);
                        }

                        break;
                    case CacheState.Replace:
                        var oldReplace = SupportFragmentManager.Fragments.LastOrDefault(w => w.GetType() == type);
                        if (oldReplace != null) DoOrNo(fragmentTransaction => transaction.Remove(oldReplace));

                        transaction.Replace(ContainerId ?? 0, fragment);
                        transaction.AddToBackStack(fragment.GetType().FullName);
                        break;
                }
            });
        }

        public void RemoveFragment(Fragment fragment)
        {
            DoOrNo(transaction =>
            {
                transaction.Remove(fragment);

                transaction.AddToBackStack(fragment.GetType().Name);
            });
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _configurationRepository = App.Container.Resolve<IConfigurationRepository>();

            SupportFragmentManager.BackStackChanged += (sender, args) =>
            {
                var lastFragment = SupportFragmentManager.Fragments.LastOrDefault();
                if (lastFragment is ITitle titleFragment) Title = titleFragment.Title;

                UpdateDrawerState();
            };
        }

        private void SetAnimation(Fragment previousFragment, Fragment fragment)
        {
            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();

            var time = appConfiguration.GetCalculationAnimationTime();
            var animationType = appConfiguration.AnimationType;

            switch (animationType)
            {
                case AnimationType.None:
                    EnterAnimateNone();
                    break;
                case AnimationType.OnlyFade:
                    EnterAnimateOnlyFade(previousFragment, fragment, time);
                    break;
                case AnimationType.ExitToBottomEnterFromBottom:
                    EnterAnimateOnlySlide(previousFragment, fragment, time);
                    break;
                case AnimationType.ExitToBottomEnterFade:
                    EnterAnimateExitToBottomEnterFade(previousFragment, fragment, time);
                    break;
                case AnimationType.ExitFadeEnterFromBottom:
                    EnterAnimateExitFadeEnterFromBottom(previousFragment, fragment, time);
                    break;
                case AnimationType.FromLeftToRight:
                    EnterAnimateFromLeftToRight(previousFragment, fragment, time);
                    break;
                case AnimationType.FromRightToLeft:
                    EnterAnimateFromRightToLeft(previousFragment, fragment, time);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void EnterAnimateNone() { }

        private void EnterAnimateOnlyFade(Fragment previousFragment, Fragment fragment, int time)
        {
            CommonAnimate(previousFragment, fragment, time, new Fade(), new Fade());
        }

        private void EnterAnimateOnlySlide(Fragment previousFragment, Fragment fragment, int time)
        {
            CommonAnimate(previousFragment, fragment, time, new Slide(), new Slide());
        }

        private void EnterAnimateExitToBottomEnterFade(Fragment previousFragment, Fragment fragment, int time)
        {
            CommonAnimate(previousFragment, fragment, time, new Slide(), new Fade());
        }

        private void EnterAnimateExitFadeEnterFromBottom(Fragment previousFragment, Fragment fragment, int time)
        {
            CommonAnimate(previousFragment, fragment, time, new Fade(), new Slide());
        }

        private void EnterAnimateFromLeftToRight(Fragment previousFragment, Fragment fragment, int time)
        {
            var enter = new Slide {SlideEdge = (int) GravityFlags.Right};
            var exit = new Slide {SlideEdge = (int) GravityFlags.Left};
            CommonAnimate(previousFragment, fragment, time, exit, enter);
        }

        private void EnterAnimateFromRightToLeft(Fragment previousFragment, Fragment fragment, int time)
        {
            var enter = new Slide {SlideEdge = (int) GravityFlags.Left};
            var exit = new Slide {SlideEdge = (int) GravityFlags.Right};
            CommonAnimate(previousFragment, fragment, time, exit, enter);
        }

        // TODO Разобраться, почему же при onbackpress нет анимации 
        private void CommonAnimate(
            Fragment previousFragment,
            Fragment fragment,
            int time,
            Visibility exit,
            Visibility enter)
        {
            var isFirstFragment = previousFragment == null;

            if (!isFirstFragment)
            {
                exit.Mode = Visibility.ModeOut;
                exit.SetDuration(time);
                previousFragment.ExitTransition = exit;
            }

            enter.Mode = Visibility.ModeIn;
            if (!isFirstFragment)
                enter.SetStartDelay(time);
            enter.SetDuration(isFirstFragment ? time : 2 * time);
            fragment.EnterTransition = enter;
        }

        private void DoOrNo(Action<FragmentTransaction> doOrNow)
        {
            if (ContainerId.HasValue)
                using (var transaction = SupportFragmentManager.BeginTransaction())
                {
                    doOrNow?.Invoke(transaction);

                    transaction.Commit();
                }
        }

        public override void OnBackPressed()
        {
            if (DrawerLayout.IsDrawerOpen(DrawerGravity))
            {
                DrawerLayout.CloseDrawer(DrawerGravity);
            }
            else
            {
                if (IsHomeToggle)
                    Finish();
                else
                    base.OnBackPressed();
            }
        }
    }

    public enum CacheState
    {
        /// <summary>
        ///     Добавляет новый фрагмент
        /// </summary>
        New,

        /// <summary>
        ///     Если есть в стеке, показывает его, если нет, создает новый
        /// </summary>
        Old,

        /// <summary>
        ///     Если есть старый, удаляет, помещает в стек новый
        /// </summary>
        Replace
    }
}
