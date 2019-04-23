using Autofac;
using Core.Api.Feedly;
using Core.Database.Rss;
using Core.Extensions;
using Core.Infrastructure.Mappers;
using Core.Repositories.Configuration;
using Core.Repositories.Feedly;
using Core.Repositories.Rss;
using Core.Repositories.RssMessage;
using Core.Services.Rss;
using Core.Services.RssMessages;

namespace Core.Container.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<RssRepository>().NotNull().As<IRssRepository>().NotNull().SingleInstance();
            builder.RegisterType<RssMessagesRepository>().NotNull().As<IRssMessagesRepository>().NotNull().SingleInstance();
            builder.RegisterType<ConfigurationRepository>().NotNull().As<IConfigurationRepository>().NotNull().SingleInstance();
            builder.RegisterType<FeedlyRepository>().NotNull().As<IFeedlyRepository>().NotNull().SingleInstance();

            builder.RegisterType<RssMapper>().NotNull().As<IMapper<RssModel, RssDomainModel>>().NotNull().SingleInstance();
            builder.RegisterType<RssMapper>().NotNull().As<IMapper<RssDomainModel, RssModel>>().NotNull().SingleInstance();
            builder.RegisterType<RssMapper>().NotNull().As<IMapper<RssDomainModel, RssServiceModel>>().NotNull().SingleInstance();
            builder.RegisterType<RssMapper>().NotNull().As<IMapper<RssServiceModel, RssDomainModel>>().NotNull().SingleInstance();

            builder.RegisterType<RssMessageMapper>().NotNull().As<IMapper<RssMessageModel, RssMessageDomainModel>>().NotNull().SingleInstance();
            builder.RegisterType<RssMessageMapper>().NotNull().As<IMapper<RssMessageDomainModel, RssMessageModel>>().NotNull().SingleInstance();
            builder.RegisterType<RssMessageMapper>().NotNull().As<IMapper<RssMessageServiceModel, RssMessageDomainModel>>().NotNull().SingleInstance();
            builder.RegisterType<RssMessageMapper>().NotNull().As<IMapper<RssMessageDomainModel, RssMessageServiceModel>>().NotNull().SingleInstance();

            builder.RegisterType<FeedlyMapper>().NotNull().As<IMapper<FeedlyRssApiModel, FeedlyRssDomainModel>>().NotNull().SingleInstance();
        }
    }
}
