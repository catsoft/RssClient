using Core.Database.Rss;
using Core.Infrastructure.Mappers;
using Core.Repositories.RssFeeds;
using Core.Services.RssMessages;
using JetBrains.Annotations;

namespace Core.Repositories.RssMessage
{
    public class RssMessageMapper : IMapper<RssMessageModel, RssMessageDomainModel>, 
        IMapper<RssMessageDomainModel, RssMessageModel>,
        IMapper<RssMessageServiceModel, RssMessageDomainModel>,
        IMapper<RssMessageDomainModel, RssMessageServiceModel>
    {
        [NotNull] private readonly IMapper<RssFeedModel, RssFeedDomainModel> _rssMapper;

        public RssMessageMapper([NotNull] IMapper<RssFeedModel, RssFeedDomainModel> rssMapper)
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
                    RssFeedParent = model.RssFeedParent,
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
                    RssFeedParent = model.RssFeedParent,
                    IsFavorite = model.IsFavorite,
                    CreationDate = model.CreationDate,
                    ImageUrl = model.ImageUrl,
                    SyndicationId = model.SyndicationId
                };
        }
    }
}
