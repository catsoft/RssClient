using Shared.Services.Navigator;

namespace Shared.ViewModels
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