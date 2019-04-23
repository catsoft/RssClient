using Core.Infrastructure.ViewModels;
using JetBrains.Annotations;

namespace Core.ViewModels.RssFeeds.Edit
{
    public class RssEditParameters : ViewModelParameters
    {
        public RssEditParameters([CanBeNull] string rssId) { RssId = rssId; }

        [CanBeNull] public string RssId { get; }
    }
}
