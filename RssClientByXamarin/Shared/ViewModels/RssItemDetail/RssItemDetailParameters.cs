using Shared.Infrastructure.ViewModels;
using Shared.Repository.Rss;
using Shared.Services.Rss;

namespace Shared.ViewModels.RssItemDetail
{
    public class RssItemDetailParameters : ViewModelParameters
    {
        public RssServiceModel RssModel { get; }

        public RssItemDetailParameters(RssServiceModel rssModel)
        {
            RssModel = rssModel;
        }
    }
}