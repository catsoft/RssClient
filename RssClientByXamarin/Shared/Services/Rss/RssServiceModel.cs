using System;

namespace Shared.Services.Rss
{
    public class RssServiceModel
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public string Rss { get; set; }
        
        public int Position { get; set; }
        
        public string UrlPreviewImage { get; set; }
        
        public DateTimeOffset CreationTime { get; set; }
        
        public DateTimeOffset? UpdateTime { get; set; }
    }
}