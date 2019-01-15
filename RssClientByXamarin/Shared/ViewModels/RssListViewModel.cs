using Droid.Infrastructure;

namespace Shared.ViewModels
{
    public class RssListViewModel : ViewModel
    {
        public abstract class Way : Way<RssListViewModel, Way.WayData>
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