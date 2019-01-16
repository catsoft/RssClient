using Android.OS;
using Android.Views;
using Droid.Screens.Navigation;

namespace Droid.Screens.Settings
{
    public class SettingsFragment : TitleFragment
    {
        public override bool RootFragment => true;
        
        public SettingsFragment()
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_settings, container, false);

            Title = Activity.GetString(Resource.String.settings_title);

            return view;
        }
    }
}