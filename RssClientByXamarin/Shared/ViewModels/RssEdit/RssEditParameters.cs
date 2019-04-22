using JetBrains.Annotations;
using Shared.Infrastructure.ViewModels;

namespace Shared.ViewModels.RssEdit
{
    public class RssEditParameters : ViewModelParameters
    {
        public RssEditParameters([CanBeNull] string rssId) { RssId = rssId; }

        [CanBeNull] public string RssId { get; }
    }
}
