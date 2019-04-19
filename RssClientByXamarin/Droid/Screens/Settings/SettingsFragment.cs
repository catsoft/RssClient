#region

using Android.OS;
using Android.Views;
using Android.Widget;
using Droid.Screens.Navigation;
using Shared.ViewModels.Settings;

#endregion

namespace Droid.Screens.Settings
{
    public class SettingsFragment : BaseFragment<SettingsViewModel>
    {
        protected override int LayoutId => Resource.Layout.fragment_settings;
        public override bool IsRoot => true;

        protected override void RestoreState(Bundle saved) { }

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
