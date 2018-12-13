using System;

namespace Database.Rss
{
    public class RssMessageModel : Entity
    {
        public string Title { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
		public string ImageUrl { get; set; }
		public bool IsRead { get; set; }
	    public bool IsDeleted { get; set; }

        public string PrimaryKeyRssModel { get; set; }
	}
}