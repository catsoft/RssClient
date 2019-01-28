using Android.Graphics;
using Android.OS;
using Android.Views;
using Droid.Screens.Navigation;

namespace Droid.Screens.Settings
{
    public class SettingsRssDetailFragment : SubFragment
    {
        protected override int LayoutId => Resource.Layout.fragment_settings_rss_detail;

        public SettingsRssDetailFragment()
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