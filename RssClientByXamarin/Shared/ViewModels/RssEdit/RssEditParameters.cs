using Shared.Infrastructure.ViewModels;

namespace Shared.ViewModels.RssEdit
{
    public class RssEditParameters : ViewModelParameters
    {
        public string RssId { get; }

        public RssEditParameters(string rssId)
        {
            RssId = rssId;
        }
    }
}