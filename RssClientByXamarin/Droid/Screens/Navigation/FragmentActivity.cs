using System;
using System.Linq;
using Android.Support.V4.App;
using Droid.Screens.Base;

namespace Droid.Screens.Navigation
{
    public abstract class FragmentActivity : ToolbarActivity
    {
        protected abstract int? ContainerId { get; }
        public void AddFragment(Fragment fragment, CacheState cacheState = CacheState.New)
        {
            DoOrNo(transaction =>
            {
                var type = fragment.GetType();

                switch (cacheState)
                {
                    case CacheState.New:
                        transaction.Add(ContainerId ?? 0, fragment);
                        transaction.AddToBackStack(fragment.GetType().FullName);
                        break;
                    case CacheState.Old:
                        var old = SupportFragmentManager.Fragments.LastOrDefault(w => w.GetType() == type);
                        if (old == null)
                        {
                            transaction.Add(ContainerId ?? 0, fragment);
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
                        transaction.Add(ContainerId ?? 0, fragment);
                        transaction.AddToBackStack(fragment.GetType().FullName);
                        break;
                }

                transaction.SetCustomAnimations(FragmentTransaction.TransitFragmentOpen,FragmentTransaction.TransitFragmentClose);

                if (fragment is TitleFragment titledFragment)
                {
                    Toolbar.Title = titledFragment.Title;
                }
            });
        }
        
        public void RemoveFragment(Fragment fragment)
        {
            DoOrNo(transaction =>
            {
                transaction.Remove(fragment);

                transaction.AddToBackStack(fragment.GetType().Name);

                transaction.SetTransition(FragmentTransaction.TransitFragmentOpen);

                if (fragment is TitleFragment titledFragment)
                {
                    Toolbar.Title = titledFragment.Title;
                }
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

        // TODO добавить работу с title
        public override void OnBackPressed()
        {
            base.OnBackPressed();
            
            if(SupportFragmentManager.Fragments.Count == 0)
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