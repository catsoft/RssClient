using Shared.Services.Navigator;

namespace Shared.ViewModels
{
    public class RssEditViewModel : ViewModel
    {
        public abstract class Way : Way<RssEditViewModel, Way.WayData>
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