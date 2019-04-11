using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;

namespace Shared.ViewModels.Close
{
    public class CloseViewModel : ViewModel
    {
        public abstract class Way : DataWay<CloseViewModel, Way.WayData>
        {
            public class WayData
            {
                
            }
        }
    }
}
