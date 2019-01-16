using System;
using System.Linq;
using Android.Animation;
using Android.OS;
using Android.Support.V4.App;
using Android.Views.Animations;
using Java.Lang;

namespace Droid.Screens.Navigation
{
    public abstract class FragmentActivity : BurgerActivity
    {
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

        public void RemoveFragment(Fragment fragment)
        {
            DoOrNo(transaction =>
            {
                transaction.Remove(fragment);

                transaction.AddToBackStack(fragment.GetType().Name);
            });
        }

        private void UpdateDrawerState()
        {
            var isSubFragment = SupportFragmentManager.BackStackEntryCount > 1;

            var from = isSubFragment ? 0 : 1;
            var to = isSubFragment ? 1 : 0;
            var anim = ValueAnimator.OfFloat(from, to);
            anim.Update += (sender, args) =>
            {
                var offset = args.Animation.AnimatedValue as Float;
                Toggle.OnDrawerSlide(DrawerLayout, offset.FloatValue());
            };
            anim.SetInterpolator(new LinearInterpolator());
            anim.SetDuration(200);
            anim.Start();

            DrawerLayout.SetDrawerLockMode(isSubFragment
                ? Android.Support.V4.Widget.DrawerLayout.LockModeLockedClosed
                : Android.Support.V4.Widget.DrawerLayout.LockModeUnlocked);
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
            base.OnBackPressed();

            if (SupportFragmentManager.Fragments.Count == 0)
                Finish();
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