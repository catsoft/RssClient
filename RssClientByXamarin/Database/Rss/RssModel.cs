using System;
using System.Collections.Generic;
using Realms;

namespace Database.Rss
{
    public class RssModel : RealmObject
    {
		[PrimaryKey]
		public string Id { get; set; }

        public string Name { get; set; }
	    public string Rss => Id;
	    public string UrlPreviewImage { get; set; }
        public DateTimeOffset CreationTime { get; set; }
		public DateTimeOffset? UpdateTime { get; set; }

		public long CountMessages { get; set; }

		public IList<RssMessageModel> RssMessageModels { get; }
    }
}
