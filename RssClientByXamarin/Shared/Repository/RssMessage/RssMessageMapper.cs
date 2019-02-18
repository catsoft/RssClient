using System.Linq;
using Shared.Database.Rss;
using Shared.Repository.Rss;

namespace Shared.Repository.RssMessage
{
    public class RssMessageMapper : IMapper<RssMessageModel, RssMessageData>
    {
        private readonly IMapper<RssModel, RssData> _rssMapper;
        
        public RssMessageMapper(IMapper<RssModel, RssData> rssMapper)
        {
            _rssMapper = rssMapper;
        }
        
        public RssMessageData Transform(RssMessageModel model)
        {
            return model == null ? new RssMessageData() : new RssMessageData()
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
    }
}