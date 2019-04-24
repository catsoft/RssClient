using System;
using Core.Infrastructure.ViewModels;
using JetBrains.Annotations;

namespace Core.ViewModels.RssFeeds.Edit
{
    public class RssEditParameters : ViewModelParameters
    {
        public RssEditParameters(Guid rssId) { RssId = rssId; }

        public Guid RssId { get; }
    }
}
