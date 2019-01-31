using Shared.Services.Navigator;

namespace Shared.ViewModels
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