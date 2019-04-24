using System;

namespace Core.Services.RssMessages
{
    public class RssMessageServiceModel
    {
        public Guid Id { get; set; }

        public string SyndicationId { get; set; }

        public string Title { get; set; }

        public DateTimeOffset CreationDate { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public bool IsRead { get; set; }

        public bool IsFavorite { get; set; }
        
        public Guid RssId { get; set; }
    }
}
