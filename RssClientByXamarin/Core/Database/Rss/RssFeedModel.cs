using System;
using SQLite;

namespace Core.Database.Rss
{
    [Table("RssFeed")]
    public class RssFeedModel : IHaveId
    {
        [PrimaryKey] public Guid Id { get; set; }
        
        public string Name { get; set; }

        public string Rss { get; set; }

        public int Position { get; set; }

        public string UrlPreviewImage { get; set; }
        
        public bool IsFeedly { get; set; }
        
        public DateTimeOffset CreationTime { get; set; }

        public DateTimeOffset? UpdateTime { get; set; }
        
        public int CountNewMessages { get; set; }
        
        public int CountAllMessages { get; set; }
    }
}
