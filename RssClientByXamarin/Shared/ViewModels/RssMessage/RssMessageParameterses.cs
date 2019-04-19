#region

using Shared.Infrastructure.ViewModels;
using Shared.Repositories.RssMessage;

#endregion

namespace Shared.ViewModels.RssMessage
{
    public class RssMessageParameterses : ViewModelParameters
    {
        public RssMessageParameterses(RssMessageDomainModel rssMessageModel) { RssMessageModel = rssMessageModel; }

        public RssMessageDomainModel RssMessageModel { get; }
    }
}
