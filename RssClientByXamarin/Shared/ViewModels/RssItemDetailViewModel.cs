using Shared.Database.Rss;
using Shared.Services.Navigator;

namespace Shared.ViewModels
{
    public class RssItemDetailViewModel : ViewModel
    {
        public abstract class Way : Way<RssItemDetailViewModel, Way.WayData>
        {
            public Way()
            {

            }

            public class WayData
            {
                public RssModel RssModel { get; set; }

                public WayData(RssModel rssModel)
                {
                    RssModel = rssModel;
                }
            }
        }
    }
}
