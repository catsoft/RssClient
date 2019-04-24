using System;
using System.Collections.Generic;
using SQLite;

namespace Core.Database.Rss
{
    public class RssModel : IHaveId
    {
        [PrimaryKey] public string Id { get; set; } = Guid.NewGuid().ToString();
        
        public string Name { get; set; }

        public string Rss { get; set; }

        public int Position { get; set; }

        public string UrlPreviewImage { get; set; }

        public DateTimeOffset CreationTime { get; set; }

        public DateTimeOffset? UpdateTime { get; set; }
    }
}
