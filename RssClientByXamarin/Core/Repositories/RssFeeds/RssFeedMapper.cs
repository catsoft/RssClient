using Core.Database.Rss;
using Core.Infrastructure.Mappers;
using Core.Services.RssFeeds;

namespace Core.Repositories.RssFeeds
{
    public class RssFeedMapper : IMapper<RssFeedModel, RssFeedDomainModel>,
        IMapper<RssFeedDomainModel, RssFeedModel>,
        IMapper<RssFeedDomainModel, RssFeedServiceModel>,
        IMapper<RssFeedServiceModel, RssFeedDomainModel>
    {
        RssFeedModel IMapper<RssFeedDomainModel, RssFeedModel>.Transform(RssFeedDomainModel model)
        {
            return model == null
                ? new RssFeedModel()
                : new RssFeedModel
                {
                    Id = model.Id,
                    
                    Rss = model.Rss,
                    Name = model.Name,
                    Position = model.Position,
                    UpdateTime = model.UpdateTime,
                    CreationTime = model.CreationTime,
                    UrlPreviewImage = model.UrlPreviewImage,
                    IsFeedly = model.IsFeedly,
                    CountNewMessages = model.CountNewMessages,
                    CountAllMessages = model.CountAllMessages,
                };
        }

        public RssFeedServiceModel Transform(RssFeedDomainModel model)
        {
            return model == null
                ? new RssFeedServiceModel()
                : new RssFeedServiceModel
                {
                    Id = model.Id,
                    Rss = model.Rss,
                    Name = model.Name,
                    Position = model.Position,
                    UpdateTime = model.UpdateTime,
                    CreationTime = model.CreationTime,
                    UrlPreviewImage = model.UrlPreviewImage,
                    IsFeedly = model.IsFeedly,
                    CountNewMessages = model.CountNewMessages,
                    CountAllMessages = model.CountAllMessages,
                };
        }

        public RssFeedDomainModel Transform(RssFeedModel model)
        {
            return model == null
                ? new RssFeedDomainModel()
                : new RssFeedDomainModel
                {
                    Id = model.Id,
                    Rss = model.Rss,
                    Name = model.Name,
                    Position = model.Position,
                    UpdateTime = model.UpdateTime,
                    CreationTime = model.CreationTime,
                    UrlPreviewImage = model.UrlPreviewImage,
                    IsFeedly = model.IsFeedly,
                    CountNewMessages = model.CountNewMessages,
                    CountAllMessages = model.CountAllMessages,
                };
        }

        public RssFeedDomainModel Transform(RssFeedServiceModel model)
        {
            return model == null
                ? new RssFeedDomainModel()
                : new RssFeedDomainModel
                {
                    Id = model.Id,
                    Rss = model.Rss,
                    Name = model.Name,
                    Position = model.Position,
                    UpdateTime = model.UpdateTime,
                    CreationTime = model.CreationTime,
                    UrlPreviewImage = model.UrlPreviewImage,
                    IsFeedly = model.IsFeedly,
                    CountNewMessages = model.CountNewMessages,
                    CountAllMessages = model.CountAllMessages,
                };
        }
    }
}
