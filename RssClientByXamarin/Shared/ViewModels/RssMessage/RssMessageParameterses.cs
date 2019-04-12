using Shared.Infrastructure.ViewModels;
using Shared.Repository.RssMessage;

namespace Shared.ViewModels.RssMessage
{
    public class RssMessageParameterses : ViewModelParameters
    {
        public RssMessageData RssMessageModel { get; }

        public RssMessageParameterses(RssMessageData rssMessageModel)
        {
            RssMessageModel = rssMessageModel;
        }
    }
}