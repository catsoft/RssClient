using System;
using Core.Database;

namespace Core.Repositories.RssFeeds
{
    public class RssFeedDomainModel : IHaveId
    {
        public string Name { get; set; }

        public string Rss { get; set; }

        public int Position { get; set; }

        public string UrlPreviewImage { get; set; }

        public DateTimeOffset CreationTime { get; set; }

        public DateTimeOffset? UpdateTime { get; set; }
        
        public bool IsFeedly { get; set; }
        
        public Guid Id { get; set; }
    }
}
