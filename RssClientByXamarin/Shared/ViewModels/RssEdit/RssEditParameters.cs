#region

using JetBrains.Annotations;
using Shared.Infrastructure.ViewModels;

#endregion

namespace Shared.ViewModels.RssEdit
{
    public class RssEditParameters : ViewModelParameters
    {
        public RssEditParameters([CanBeNull] string rssId) => RssId = rssId;

        [CanBeNull] 
        public string RssId { get; }
    }
}
