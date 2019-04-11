using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;

namespace Shared.ViewModels.Contacts
{
    public class ContactsViewModel : ViewModel
    {
        public abstract class Way : DataWay<ContactsViewModel, Way.WayData>
        {
            public class WayData
            {
            
            }
        }
    }
}