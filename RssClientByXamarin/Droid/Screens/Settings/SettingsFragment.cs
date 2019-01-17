using Android.OS;
using Android.Views;
using Droid.Screens.Navigation;

namespace Droid.Screens.Settings
{
    public class SettingsFragment : TitleFragment
    {
        protected override int LayoutId => Resource.Layout.fragment_settings;
        public override bool RootFragment => true;
        
        public SettingsFragment()
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            Title = Activity.GetString(Resource.String.settings_title);

            return view;
        }
    }
}