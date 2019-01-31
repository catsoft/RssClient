using Shared.Database.Rss;
using Shared.Services.Navigator;

namespace Shared.ViewModels
{
    public class RssMessageViewModel : ViewModel
    {
        public abstract class Way : DataWay<RssAllMessagesViewModel, Way.WayData>
        {
            public class WayData
            {
                public RssMessageModel RssMessageModel { get; }

                public WayData(RssMessageModel rssMessageModel)
                {
                    RssMessageModel = rssMessageModel;
                }
            }
        }
    }
}