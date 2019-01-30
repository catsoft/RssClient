using Android.OS;
using Android.Views;
using Android.Widget;
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

        protected override void RestoreState(Bundle saved)
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            Title = Activity.GetString(Resource.String.settings_title);

            var scrollView = view.FindViewById<ScrollView>(Resource.Id.scrollView_settings_main);
            scrollView.SaveEnabled = true;
            
            return view;
        }
    }
}