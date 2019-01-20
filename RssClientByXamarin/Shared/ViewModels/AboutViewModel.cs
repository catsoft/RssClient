using Shared.Services.Navigator;

namespace Shared.ViewModels
{
    public class AboutViewModel : ViewModel
    {
        public abstract class Way : Way<AboutViewModel, Way.WayData>
        {

            public Way()
            {
                
            }
            
            public class WayData
            {
            
            }
        }
    }
}