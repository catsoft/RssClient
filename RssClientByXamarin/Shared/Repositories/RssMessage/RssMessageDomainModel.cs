using System;
using JetBrains.Annotations;
using Shared.Database;
using Shared.Repositories.Rss;

namespace Shared.Repositories.RssMessage
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

        [CanBeNull] public RssDomainModel RssParent { get; set; }

        public string Id { get; set; }
    }
}
