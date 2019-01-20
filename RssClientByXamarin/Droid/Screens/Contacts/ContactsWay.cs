using Droid.Screens.About;
using Droid.Screens.Navigation;
using Shared.ViewModels;

namespace Droid.Screens.Contacts
{
    public class ContactsWay : RssItemDetailViewModel.Way
    {
        private readonly FragmentActivity _fragmentActivity;

        public ContactsWay(FragmentActivity fragmentActivity)
        {
            _fragmentActivity = fragmentActivity;
        }

        public override void Go()
        {
            var fragment = new ContactsFragment();

            _fragmentActivity.AddFragment(fragment, CacheState.Replace);
        }
    }
}