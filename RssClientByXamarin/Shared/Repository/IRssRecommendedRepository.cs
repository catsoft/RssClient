using System.Collections.Generic;
using System.Linq;
using Shared.Database.Rss;

namespace Droid.Repository
{
    public interface IRssRecommendedRepository
    {
        IQueryable<RssRecommendationModel> GetAll();
        IQueryable<RssRecommendationModel> GetAllByCategory(Categories categories);
        IEnumerable<Categories> GetCategories();
    }
}