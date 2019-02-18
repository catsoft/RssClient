using Shared.Database.Rss;

namespace Shared.Repository.RssRecommended
{
    public class RssRecommendedData : IHaveId
    {
        public string Id { get; set; }
        public string Rss { get; set; }
        public Categories Category { get; set; }
        public int Position { get; set; }
    }
}