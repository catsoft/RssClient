using System;
using Core.Api.Feedly;
using Core.Infrastructure.Mappers;

namespace Core.Repositories.Feedly
{
    public class FeedlyMapper : IMapper<FeedlyRssApiModel, FeedlyRssDomainModel>
    {
        public FeedlyRssDomainModel Transform(FeedlyRssApiModel model)
        {
            return model == null
                ? new FeedlyRssDomainModel()
                : new FeedlyRssDomainModel
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
