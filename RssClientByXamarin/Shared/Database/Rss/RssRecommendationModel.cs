using System;
using Realms;

namespace Shared.Database.Rss
{
    public class RssRecommendationModel : RealmObject
    {
        [PrimaryKey] public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Rss { get; set; }

        public int CategoryInt { get; set; }

        [Ignored]
        public Categories Category
        {
            get => (Categories) CategoryInt;
            set => CategoryInt = (int) value;
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