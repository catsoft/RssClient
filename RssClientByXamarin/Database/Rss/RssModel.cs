using System;
using System.Collections.Generic;

namespace Database.Rss
{
    public class RssModel : Entity
    {
        public string Name { get; set; }
        public string Rss { get; set; }
	    public string UrlPreviewImage { get; set; }
        public DateTime CreationTime { get; set; }
		public DateTime? UpdateTime { get; set; }

        [SQLite.Ignore]
        public List<RssMessageModel> Messages { get; set; }
    }
}
