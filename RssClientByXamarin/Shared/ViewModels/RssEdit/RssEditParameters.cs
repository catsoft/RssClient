#region

using Shared.Infrastructure.ViewModels;

#endregion

namespace Shared.ViewModels.RssEdit
{
    public class RssEditParameters : ViewModelParameters
    {
        public RssEditParameters(string rssId) { RssId = rssId; }

        public string RssId { get; }
    }
}
