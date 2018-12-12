using System;
using System.Collections.Generic;

namespace Database.Rss
{
    public class RssModel : Entity
    {
        public string Name { get; set; }
	    public string Rss => Id;
	    public string UrlPreviewImage { get; set; }
        public DateTime CreationTime { get; set; }
		public DateTime? UpdateTime { get; set; }

		public long CountMessages { get; set; }
    }
}
