using System;
using Shared.Database.Rss;

namespace Shared.Repository.Rss
{
    public class RssMapper : IMapper<RssModel, RssData>
    {
        public RssData Transform(RssModel model)
        {
            return model == null ? new RssData() : new RssData()
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