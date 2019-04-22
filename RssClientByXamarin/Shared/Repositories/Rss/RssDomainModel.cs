using System;
using Shared.Database;

namespace Shared.Repositories.Rss
{
    public class RssDomainModel : IHaveId
    {
        public string Name { get; set; }

        public string Rss { get; set; }

        public int Position { get; set; }

        public string UrlPreviewImage { get; set; }

        public DateTimeOffset CreationTime { get; set; }

        public DateTimeOffset? UpdateTime { get; set; }
        public string Id { get; set; }
    }
}
