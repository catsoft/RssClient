using System.Collections.Generic;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
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

            var speedSpinner = view.FindViewById<AppCompatSpinner>(Resource.Id.appCompatSpinner_settingsStartPage_speedAnimation);
            var typeSpinner = view.FindViewById<AppCompatSpinner>(Resource.Id.appCompatSpinner_settingsStartPage_typeAnimation);
            
            speedSpinner.Adapter = new ArrayAdapter(Context, Resource.Layout.support_simple_spinner_dropdown_item, new List<string>()
            {
                "0.25x",
                "0.5x",
                "1x",
                "2x",
                "4x",
                "8x"
            });
            
            typeSpinner.Adapter = new ArrayAdapter(Context, Resource.Layout.support_simple_spinner_dropdown_item, new List<string>()
            {
                "None",
                "Fade",
                "From left",
                "From right",
                "From bottom"
            });
            
            return view;
        }
    }
}