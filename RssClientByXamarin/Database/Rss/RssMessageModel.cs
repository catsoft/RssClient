using System;

namespace Database.Rss
{
    public class RssMessageModel : Entity
    {
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }

        public int PrimaryKeyRssModel { get; set; }
    }
}