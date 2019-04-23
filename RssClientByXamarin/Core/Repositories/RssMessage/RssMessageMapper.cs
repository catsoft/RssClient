using System.Linq;
using Core.Database.Rss;
using Core.Infrastructure.Mappers;
using Core.Repositories.Rss;
using Core.Services.RssMessages;
using JetBrains.Annotations;

namespace Core.Repositories.RssMessage
{
    public class RssMessageMapper : IMapper<RssMessageModel, RssMessageDomainModel>, 
        IMapper<RssMessageDomainModel, RssMessageModel>,
        IMapper<RssMessageServiceModel, RssMessageDomainModel>,
        IMapper<RssMessageDomainModel, RssMessageServiceModel>
    {
        [NotNull] private readonly IMapper<RssModel, RssDomainModel> _rssMapper;

        public RssMessageMapper([NotNull] IMapper<RssModel, RssDomainModel> rssMapper)
        {
            _rssMapper = rssMapper;
        }

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

        public RssMessageDomainModel Transform(RssMessageServiceModel model)
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
                    RssParent = model.RssParent,
                    IsFavorite = model.IsFavorite,
                    CreationDate = model.CreationDate,
                    ImageUrl = model.ImageUrl,
                    SyndicationId = model.SyndicationId
                };
        }

        RssMessageServiceModel IMapper<RssMessageDomainModel, RssMessageServiceModel>.Transform(RssMessageDomainModel model)
        {
            return model == null
                ? new RssMessageServiceModel()
                : new RssMessageServiceModel
                {
                    Id = model.Id,
                    Url = model.Url,
                    Text = model.Text,
                    Title = model.Title,
                    IsRead = model.IsRead,
                    RssParent = model.RssParent,
                    IsFavorite = model.IsFavorite,
                    CreationDate = model.CreationDate,
                    ImageUrl = model.ImageUrl,
                    SyndicationId = model.SyndicationId
                };
        }
    }
}
