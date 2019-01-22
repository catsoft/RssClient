using System;
using Realms;

namespace Shared.Database.Rss
{
    public class RssRecommendationModel : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        public string Rss { get; set; }

        private int _category;
        
        [Ignored]
        public Categories Category
        {
            get => (Categories) _category;
            set => _category = (int) value;
        }
        
        public int Position { get; set; }

        public RssRecommendationModel()
        {
            
        }

        public RssRecommendationModel(string rss, Categories category = Categories.None, int position = 0)
        {
            Rss = rss;
            Category = category;
            Position = position;
        }
    }
}