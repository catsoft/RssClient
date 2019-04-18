using Shared.Infrastructure.ViewModels;
using Shared.Repository.RssMessage;

namespace Shared.ViewModels.RssMessage
{
    public class RssMessageParameterses : ViewModelParameters
    {
        public RssMessageDomainModel RssMessageModel { get; }

        public RssMessageParameterses(RssMessageDomainModel rssMessageModel)
        {
            RssMessageModel = rssMessageModel;
        }
    }
}