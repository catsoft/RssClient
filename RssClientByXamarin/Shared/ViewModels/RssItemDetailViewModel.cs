using Shared.Database.Rss;
using Shared.Services.Navigator;

namespace Shared.ViewModels
{
    public class RssItemDetailViewModel : ViewModel
    {
        public abstract class Way : DataWay<RssItemDetailViewModel, Way.WayData>
        {
            public class WayData
            {
                public RssModel RssModel { get; }

                public WayData(RssModel rssModel)
                {
                    RssModel = rssModel;
                }
            }
        }
    }
}
