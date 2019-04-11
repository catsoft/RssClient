using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;

namespace Shared.ViewModels.RssList
{
    public class RssListViewModel : ViewModel
    {
        public abstract class Way : DataWay<RssListViewModel, Way.WayData>
        {
            public class WayData
            {
            
            }
        }
    }
}