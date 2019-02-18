using Shared.Database.Rss;

namespace Shared.Repository.RssRecommended
{
    public class RssRecommendedMapper : IMapper<RssRecommendationModel, RssRecommendedData>
    {
        public RssRecommendedData Transform(RssRecommendationModel model)
        {
            return model == null ? new RssRecommendedData() : new RssRecommendedData()
            {
                Id = model.Id,
                Rss = model.Rss,
                Category = model.Category,
                Position = model.Position,
            };
        }
    }
}