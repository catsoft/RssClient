using Android.Support.V4.App;

namespace Droid.Screens.Navigation
{
    public interface IFragmentManager
    {
        void AddFragment(Fragment fragment, CacheState cacheState = CacheState.New);

        void RemoveFragment(Fragment fragment);
    }
}