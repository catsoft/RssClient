using Android.OS;
using Android.Support.V4.App;
using Android.Views;

namespace Droid.Screens.Rss.Settings
{
    public class SettingsFragment : Fragment
    {
        public SettingsFragment()
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_settings, container, false);

            return view;
        }
    }
}