#region

using System;
using Shared.Repositories.Rss;

#endregion

namespace Shared.Database.Rss
{
    public class RssMessageServiceModel
    {
        public string Id { get; set; }

        public string SyndicationId { get; set; }

        public string Title { get; set; }

        public DateTimeOffset CreationDate { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public bool IsRead { get; set; }

        public bool IsFavorite { get; set; }

        public RssDomainModel RssParent { get; set; }
    }
}
