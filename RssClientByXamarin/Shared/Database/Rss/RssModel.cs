using System;
using System.Collections.Generic;
using Realms;

namespace Shared.Database.Rss
{
    public class RssModel : RealmObject, IHaveId
    {
		[PrimaryKey]
		public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }
	    public string Rss { get; set; }
	    public string UrlPreviewImage { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public DateTimeOffset? UpdateTime { get; set; }

		public IList<RssMessageModel> RssMessageModels { get; }
    }
}
