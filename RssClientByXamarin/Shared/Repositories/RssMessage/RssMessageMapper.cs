#region

using System.Linq;
using JetBrains.Annotations;
using Shared.Database.Rss;
using Shared.Infrastructure.Mappers;
using Shared.Repositories.Rss;

#endregion

namespace Shared.Repositories.RssMessage
{
    public class RssMessageMapper : IMapper<RssMessageModel, RssMessageDomainModel>, IMapper<RssMessageDomainModel, RssMessageModel>
    {
        [NotNull] private readonly IMapper<RssModel, RssDomainModel> _rssMapper;

        public RssMessageMapper([NotNull] IMapper<RssModel, RssDomainModel> rssMapper) { _rssMapper = rssMapper; }

        public RssMessageModel Transform(RssMessageDomainModel model)
        {
            return model == null
                ? new RssMessageModel()
                : new RssMessageModel
                {
                    Id = model.Id,
                    Url = model.Url,
                    Text = model.Text,
                    Title = model.Title,
                    IsRead = model.IsRead,
                    IsFavorite = model.IsFavorite,
                    CreationDate = model.CreationDate,
                    ImageUrl = model.ImageUrl,
                    SyndicationId = model.SyndicationId
                };
        }

        public RssMessageDomainModel Transform(RssMessageModel model)
        {
            return model == null
                ? new RssMessageDomainModel()
                : new RssMessageDomainModel
                {
                    Id = model.Id,
                    Url = model.Url,
                    Text = model.Text,
                    Title = model.Title,
                    IsRead = model.IsRead,
                    RssParent = _rssMapper.Transform(model.RssParent?.FirstOrDefault()),
                    IsFavorite = model.IsFavorite,
                    CreationDate = model.CreationDate,
                    ImageUrl = model.ImageUrl,
                    SyndicationId = model.SyndicationId
                };
        }
    }
}
