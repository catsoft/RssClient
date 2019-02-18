using System.Collections.Generic;
using Shared.Database.Rss;

namespace Shared.Repository.RssRecommended
{
    public interface IRssRecommendedRepository
    {
        IEnumerable<RssRecommendedData> GetAll();
        IEnumerable<RssRecommendedData> GetAllByCategory(Categories categories);
        IEnumerable<Categories> GetCategories();
    }
}