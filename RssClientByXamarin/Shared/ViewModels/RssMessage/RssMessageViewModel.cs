using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;
using Shared.Repository.RssMessage;
using Shared.ViewModels.RssAllMessages;

namespace Shared.ViewModels.RssMessage
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