using Shared.Services.Navigator;

namespace Shared.ViewModels
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