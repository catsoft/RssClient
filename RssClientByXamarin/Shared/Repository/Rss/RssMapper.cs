using Shared.Database.Rss;
using Shared.Infrastructure.Mappers;
using Shared.Services.Rss;

namespace Shared.Repository.Rss
{
    public class RssMapper : IMapper<RssModel, RssDomainModel>, 
        IMapper<RssDomainModel, RssModel>, 
        IMapper<RssDomainModel, RssServiceModel>,
        IMapper<RssServiceModel, RssDomainModel>
        
    {
        public RssDomainModel Transform(RssModel model)
        {
            return model == null
                ? new RssDomainModel()
                : new RssDomainModel()
                {
                    Id = model.Id,
                    Rss = model.Rss,
                    Name = model.Name,
                    Position = model.Position,
                    UpdateTime = model.UpdateTime,
                    CreationTime = model.CreationTime,
                    UrlPreviewImage = model.UrlPreviewImage,
                };
        }

        RssModel IMapper<RssDomainModel, RssModel>.Transform(RssDomainModel model)
        {
            return model == null
                ? new RssModel()
                : new RssModel()
                {
                    Id = model.Id,
                    Rss = model.Rss,
                    Name = model.Name,
                    Position = model.Position,
                    UpdateTime = model.UpdateTime,
                    CreationTime = model.CreationTime,
                    UrlPreviewImage = model.UrlPreviewImage,
                };
        }
        
        public RssServiceModel Transform(RssDomainModel model)
        {
            return model == null
                ? new RssServiceModel()
                : new RssServiceModel()
                {
                    Id = model.Id,
                    Rss = model.Rss,
                    Name = model.Name,
                    Position = model.Position,
                    UpdateTime = model.UpdateTime,
                    CreationTime = model.CreationTime,
                    UrlPreviewImage = model.UrlPreviewImage,
                };
        }

        public RssDomainModel Transform(RssServiceModel model)
        {
            return model == null
                ? new RssDomainModel()
                : new RssDomainModel()
                {
                    Id = model.Id,
                    Rss = model.Rss,
                    Name = model.Name,
                    Position = model.Position,
                    UpdateTime = model.UpdateTime,
                    CreationTime = model.CreationTime,
                    UrlPreviewImage = model.UrlPreviewImage,
                };
        }
    }
}