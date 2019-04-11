using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;

namespace Shared.ViewModels.About
{
    public class AboutViewModel : ViewModel
    {
        public abstract class Way : DataWay<AboutViewModel, Way.WayData>
        {
            public class WayData
            {
            
            }
        }
    }
}