#region

using Android.OS;
using Android.Views;
using Droid.Screens.Navigation;
using Shared.ViewModels.About;

#endregion

namespace Droid.Screens.About
{
    public class AboutFragment : BaseFragment<AboutViewModel>
    {
        // ReSharper disable once EmptyConstructor
        public AboutFragment() { }

        protected override int LayoutId => Resource.Layout.fragment_about;
        public override bool IsRoot => true;

        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            Title = GetText(Resource.String.about_title);

            return view;
        }
    }
}
