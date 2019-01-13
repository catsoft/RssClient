using Android.Support.V4.App;
using Droid.Screens.Base;

namespace Droid.Screens.Navigation
{
    public abstract class FragmentActivity : ToolbarActivity
    {
        protected abstract int? ContainerId { get; }
        protected Fragment ActiveFragment { get; private set; }

        public void AddFragment(Fragment fragment)
        {
            if (ContainerId.HasValue)
            {
                ActiveFragment = fragment;

                var transaction = SupportFragmentManager.BeginTransaction();

                if (ActiveFragment == null)
                {
                    transaction.Add(ContainerId.Value, ActiveFragment);
                }
                else
                {
                    transaction.Replace(ContainerId.Value, ActiveFragment);
                }

                transaction.AddToBackStack(fragment.GetType().Name).Commit();

                if (fragment is TitleFragment titledFragment)
                {
                    Toolbar.Title = titledFragment.Title;
                }
            }
        }
    }
}