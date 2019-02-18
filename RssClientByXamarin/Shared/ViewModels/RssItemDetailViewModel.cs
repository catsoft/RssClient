using Shared.Database.Rss;
using Shared.Repository.Rss;
using Shared.Services.Navigator;

namespace Shared.ViewModels
{
    public class RssItemDetailViewModel : ViewModel
    {
        public abstract class Way : DataWay<RssItemDetailViewModel, Way.WayData>
        {
            public class WayData
            {
                public RssData RssModel { get; }

                public WayData(RssData rssModel)
                {
                    RssModel = rssModel;
                }
            }
        }
    }
}
