using Droid.Screens.Navigation;
using Shared.Infrastructure.Navigation;
using Shared.ViewModels.Contacts;

namespace Droid.Screens.Contacts
{
    public class ContactsWay : IWay<ContactsViewModel>
    {
        private readonly FragmentActivity _fragmentActivity;

        public ContactsWay(FragmentActivity fragmentActivity)
        {
            _fragmentActivity = fragmentActivity;
        }

        public void Go()
        {
            var fragment = new ContactsFragment();

            _fragmentActivity.AddFragment(fragment, CacheState.Replace);
        }
    }
}