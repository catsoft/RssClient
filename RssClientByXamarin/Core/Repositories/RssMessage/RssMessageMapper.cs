using Core.CoreServices.Html;
using Core.Database.Rss;
using Core.Infrastructure.Mappers;
using Core.Services.RssMessages;

namespace Core.Repositories.RssMessage
{
    public class RssMessageMapper : IMapper<RssMessageModel, RssMessageDomainModel>, 
        IMapper<RssMessageDomainModel, RssMessageModel>,
        IMapper<RssMessageServiceModel, RssMessageDomainModel>,
        IMapper<RssMessageDomainModel, RssMessageServiceModel>
    {
        private readonly IHtmlConfigurator _htmlConfigurator;

        public RssMessageMapper(IHtmlConfigurator htmlConfigurator)
        {
            _htmlConfigurator = htmlConfigurator;
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
                    SyndicationId = model.SyndicationId,
                    RssId = model.RssId,
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
                    SyndicationId = model.SyndicationId,
                    RssId = model.RssId,
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
                    IsFavorite = model.IsFavorite,
                    CreationDate = model.CreationDate,
                    ImageUrl = model.ImageUrl,
                    SyndicationId = model.SyndicationId,
                    RssId = model.RssId,
                    RssTitle = model.RssTitle,
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
                    TextHtml = _htmlConfigurator.ConfigureHtml(model.Text),
                    Title = model.Title,
                    IsRead = model.IsRead,
                    IsFavorite = model.IsFavorite,
                    CreationDate = model.CreationDate,
                    ImageUrl = model.ImageUrl,
                    SyndicationId = model.SyndicationId,
                    RssId = model.RssId,
                    RssTitle = model.RssTitle,
                };
        }
    }
}
