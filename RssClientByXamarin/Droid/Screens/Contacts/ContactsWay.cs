using Core.Infrastructure.Navigation;
using Core.ViewModels.Contacts;
using Droid.Screens.Navigation;

namespace Droid.Screens.Contacts
{
    public class ContactsWay : IWay<ContactsViewModel>
    {
        private readonly IFragmentManager _fragmentActivity;

        public ContactsWay(IFragmentManager fragmentActivity) { _fragmentActivity = fragmentActivity; }

        public void Go()
        {
            var fragment = new ContactsFragment();

            _fragmentActivity.AddFragment(fragment);
        }
    }
}
