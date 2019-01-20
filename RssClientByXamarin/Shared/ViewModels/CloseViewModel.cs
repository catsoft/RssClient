using Shared.Services.Navigator;

namespace Shared.ViewModels
{
    public class CloseViewModel : ViewModel
    {
        public abstract class Way : Way<CloseViewModel, Way.WayData>
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
