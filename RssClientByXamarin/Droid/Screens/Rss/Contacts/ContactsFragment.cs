using Android.OS;
using Android.Support.V4.App;
using Android.Views;

namespace Droid.Screens.Rss.Contacts
{
    public class ContactsFragment : Fragment
    {
        public ContactsFragment()
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_contacts, container, false);

            return view;
        }
    }
}