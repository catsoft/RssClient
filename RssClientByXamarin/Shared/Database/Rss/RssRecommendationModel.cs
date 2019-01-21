using System;
using Realms;

namespace Shared.Database.Rss
{
    public class RssRecommendationModel : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        public string Rss { get; set; }
        public string Category { get; set; }
        
        public int Position { get; set; }

        public RssRecommendationModel()
        {
            
        }

        public RssRecommendationModel(string rss, string category = null, int position = 0)
        {
            Rss = rss;
            Category = category;
            Position = position;
        }
    }
}