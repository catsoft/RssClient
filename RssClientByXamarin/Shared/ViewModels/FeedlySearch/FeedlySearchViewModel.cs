using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;

namespace Shared.ViewModels.FeedlySearch
{
    public class FeedlySearchViewModel : ViewModel
    {
        public abstract class Way : DataWay<FeedlySearchViewModel, Way.WayData>
        {
            public class WayData
            {

            }
        }
    }
}