using System;
using Core.Infrastructure.ViewModels;

namespace Core.ViewModels.RssFeeds.Edit
{
    public class RssEditParameters : ViewModelParameters
    {
        public RssEditParameters(Guid rssId) { RssId = rssId; }

        public Guid RssId { get; }
    }
}
