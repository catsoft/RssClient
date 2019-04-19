using System.Linq;
using Shared.Database.Rss;
using Shared.Infrastructure.Mappers;
using Shared.Repository.Rss;

namespace Shared.Repository.RssMessage
{
    public class RssMessageMapper : IMapper<RssMessageModel, RssMessageDomainModel>, IMapper<RssMessageDomainModel, RssMessageModel>
    {
        private readonly IMapper<RssModel, RssDomainModel> _rssMapper;
        
        public RssMessageMapper(IMapper<RssModel, RssDomainModel> rssMapper)
        {
            _rssMapper = rssMapper;
        }
        
        public RssMessageDomainModel Transform(RssMessageModel model)
        {
            return model == null ? new RssMessageDomainModel() : new RssMessageDomainModel()
            {
                Id = model.Id,
                Url = model.Url,
                Text = model.Text,
                Title = model.Title,
                IsRead = model.IsRead,
                RssParent = _rssMapper.Transform(model.RssParent.FirstOrDefault()),
                IsFavorite = model.IsFavorite,
                CreationDate = model.CreationDate,
                ImageUrl = model.ImageUrl,
                SyndicationId = model.SyndicationId,
            };
        }

        public RssMessageModel Transform(RssMessageDomainModel model)
        {
            return model == null ? new RssMessageModel() : new RssMessageModel()
            {
                Id = model.Id,
                Url = model.Url,
                Text = model.Text,
                Title = model.Title,
                IsRead = model.IsRead,
                IsFavorite = model.IsFavorite,
                CreationDate = model.CreationDate,
                ImageUrl = model.ImageUrl,
                SyndicationId = model.SyndicationId,
            };
        }
    }
}