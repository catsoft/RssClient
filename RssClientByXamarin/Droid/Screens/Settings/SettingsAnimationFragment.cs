using System.Collections.Generic;
using System.Linq;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Droid.Screens.Navigation;
using Shared.Configuration;
using Shared.Utils;

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
            
            speedSpinner.Adapter = new ArrayAdapter(Context, Resource.Layout.support_simple_spinner_dropdown_item, new List<AnimationSpeed>()
            {
                AnimationSpeed.x0_25,
                AnimationSpeed.x0_5,
                AnimationSpeed.x,
                AnimationSpeed.x2,
                AnimationSpeed.x4,
                AnimationSpeed.x8,
            }.Select(w => w.ToLocaleString()).ToList());
            
            typeSpinner.Adapter = new ArrayAdapter(Context, Resource.Layout.support_simple_spinner_dropdown_item, new List<AnimationType>()
            {
                AnimationType.None,
                AnimationType.Fade,
                AnimationType.From_left,
                AnimationType.From_right,
                AnimationType.From_bottom,
            }.Select(w => w.ToLocaleString()).ToList());
            
            return view;
        }
    }
}