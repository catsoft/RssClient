using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;

namespace Shared.ViewModels.RssCreate
{
    public class RssCreateViewModel : ViewModel
    {
        public abstract class Way : DataWay<RssCreateViewModel, Way.WayData>
        {
            public class WayData
            {
            }
        }
    }
}