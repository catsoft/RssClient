using Shared.Services.Navigator;

namespace Shared.ViewModels
{
    public class RssCreateViewModel : ViewModel
    {
        public abstract class Way : Way<RssCreateViewModel, Way.WayData>
        {
            public Way()
            {
                
            }
            
            public class WayData
            {
                public string RssId { get; set; }

                public WayData(string rssId)
                {
                    RssId = rssId;
                }
            }
        }
    }
}