using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;
using Shared.Repository.Rss;

namespace Shared.ViewModels.RssItemDetail
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
