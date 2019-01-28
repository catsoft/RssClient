using Shared.Database.Rss;
using Shared.Services.Navigator;

namespace Shared.ViewModels
{
    public class RssMessageViewModel : ViewModel
    {
        public abstract class Way : Way<RssMessageViewModel, Way.WayData>
        {

            public Way()
            {
                
            }
            
            public class WayData
            {
                public RssMessageModel RssMessageModel { get; set; }

                public WayData()
                {
                    
                }
                
                public WayData(RssMessageModel rssMessageModel)
                {
                    RssMessageModel = rssMessageModel;
                }
            }
        }
    }
}