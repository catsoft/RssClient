using Android.OS;
using Android.Views;
using Droid.Screens.Navigation;

namespace Droid.Screens.Contacts
{
    public class ContactsFragment : TitleFragment
    {
        public ContactsFragment()
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_contacts, container, false);

            Title = Activity.GetString(Resource.String.contacts_title);

            return view;
        }
    }
}