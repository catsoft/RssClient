using Android.OS;
using Android.Views;
using Droid.Screens.Navigation;

namespace Droid.Screens.Contacts
{
    public class ContactsFragment : TitleFragment
    {
        protected override int LayoutId => Resource.Layout.fragment_contacts;
        public override bool RootFragment => true;
        
        public ContactsFragment()
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            Title = Activity.GetString(Resource.String.contacts_title);

            return view;
        }
    }
}