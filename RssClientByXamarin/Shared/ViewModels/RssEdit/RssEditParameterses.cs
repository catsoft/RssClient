using Shared.Infrastructure.ViewModels;

namespace Shared.ViewModels.RssEdit
{
    public class RssEditParameterses : ViewModelParameters
    {
        public string RssId { get; }

        public RssEditParameterses(string rssId)
        {
            RssId = rssId;
        }
    }
}