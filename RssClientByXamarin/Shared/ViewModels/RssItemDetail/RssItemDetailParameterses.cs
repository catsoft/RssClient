using Shared.Infrastructure.ViewModels;
using Shared.Repository.Rss;

namespace Shared.ViewModels.RssItemDetail
{
    public class RssItemDetailParameterses : ViewModelParameters
    {
        public RssData RssModel { get; }

        public RssItemDetailParameterses(RssData rssModel)
        {
            RssModel = rssModel;
        }
    }
}