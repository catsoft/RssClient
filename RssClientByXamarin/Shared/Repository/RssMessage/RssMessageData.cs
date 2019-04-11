using System;
using Shared.Database.Rss;
using Shared.Repository.Rss;

namespace Shared.Repository.RssMessage
{
    public class RssMessageData : IHaveId
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
        
        public RssData RssParent { get; set; }
    }
}