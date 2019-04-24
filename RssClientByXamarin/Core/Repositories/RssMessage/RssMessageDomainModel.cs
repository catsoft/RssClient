using System;
using Core.Database;
using Core.Repositories.RssFeeds;
using JetBrains.Annotations;

namespace Core.Repositories.RssMessage
{
    public class RssMessageDomainModel : IHaveId
    {
        public string SyndicationId { get; set; }

        public string Title { get; set; }

        public DateTimeOffset CreationDate { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public bool IsRead { get; set; }

        public bool IsFavorite { get; set; }

        [CanBeNull] public RssFeedDomainModel RssFeedParent { get; set; }

        public Guid Id { get; set; }
    }
}
