using Shared.Infrastructure.Mappers;

namespace Shared.Repository.Feedly
{
    public class FeedlyMapper : IMapper<Api.Feedly.FeedlyRss, FeedlyRss>
    {
        public FeedlyRss Transform(Api.Feedly.FeedlyRss model)
        {
            return model == null
                ? new FeedlyRss()
                : new FeedlyRss()
                {
                    // TODO may be automapper?
                    DeliciousTags = model.DeliciousTags,
                    Title = model.Title,
                    Art = model.Art,
                    Hint = model.Hint,
                    Score = model.Score,
                    Scheme = model.Scheme,
                    Partial = model.Partial,
                    Website = model.Website,
                    Coverage = model.Coverage,
                    Language = model.Language,
                    Velocity = model.Velocity,
                    Description = model.Description,
                    Subscribers = model.Subscribers,
                    FeedId = model.FeedId,
                    IconUrl = model.IconUrl,
                    CoverUrl = model.CoverUrl,
                    VisualUrl = model.VisualUrl,
                    CoverColor = model.CoverColor,
                    ContentType = model.ContentType,
                    CoverageScore = model.CoverageScore,
                    EstimatedEngagement = model.EstimatedEngagement,
                };
        }
    }
}