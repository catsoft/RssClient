using Android.OS;
using Android.Views;
using Droid.Screens.Navigation;
using Java.Lang;

namespace Droid.Screens.About
{
    public class AboutFragment : TitleFragment
    {
        protected override int LayoutId => Resource.Layout.fragment_about;
        public override bool RootFragment => true;

        public AboutFragment()
        {
            
        }
        
        protected override void RestoreState(Bundle saved)
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            Title = GetText(Resource.String.about_title);
            
            return view;
        }
    }
}