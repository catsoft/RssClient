using Android.OS;
using Android.Views;
using Droid.Screens.Navigation;

namespace Droid.Screens.Settings
{
    public class SettingsAnimationFragment : SubFragment
    {
        protected override int LayoutId => Resource.Layout.fragment_settings_animation;

        public SettingsAnimationFragment()
        {
            
        }
        
        protected override void RestoreState(Bundle saved)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            return view;
        }
    }
}