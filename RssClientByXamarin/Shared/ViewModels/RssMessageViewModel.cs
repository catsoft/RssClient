using Shared.Database.Rss;
using Shared.Repository.RssMessage;
using Shared.Services.Navigator;

namespace Shared.ViewModels
{
    public class RssMessageViewModel : ViewModel
    {
        public abstract class Way : DataWay<RssAllMessagesViewModel, Way.WayData>
        {
            public class WayData
            {
                public RssMessageData RssMessageModel { get; }

                public WayData(RssMessageData rssMessageModel)
                {
                    RssMessageModel = rssMessageModel;
                }
            }
        }
    }
}