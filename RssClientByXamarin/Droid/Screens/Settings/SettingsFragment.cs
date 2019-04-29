using Android.OS;
using Android.Views;
using Core.Extensions;
using Core.ViewModels.Settings;
using Droid.Screens.Navigation;
using JetBrains.Annotations;

namespace Droid.Screens.Settings
{
    public class SettingsFragment : BaseFragment<SettingsViewModel>
    {
        // ReSharper disable once NotNullMemberIsNotInitialized
        [NotNull] private SettingsFragmentViewHolder _viewHolder;
        
        protected override int LayoutId => Resource.Layout.fragment_settings;
        public override bool IsRoot => true;

        // ReSharper disable once EmptyConstructor
        // ReSharper disable once NotNullMemberIsNotInitialized
        public SettingsFragment() { }
        
        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState).NotNull();

            _viewHolder = new SettingsFragmentViewHolder(view);
            
            Title = Activity.GetString(Resource.String.settings_title);

            _viewHolder.ScrollView.SaveEnabled = true;

            return view;
        }
    }
}
