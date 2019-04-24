using Autofac;
using Core.Api.Feedly;
using Core.Database.Rss;
using Core.Extensions;
using Core.Infrastructure.Mappers;
using Core.Repositories.Configurations;
using Core.Repositories.Feedly;
using Core.Repositories.RssFeeds;
using Core.Repositories.RssMessage;
using Core.Services.RssFeeds;
using Core.Services.RssMessages;

namespace Core.Container.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<RssFeedFeedRepository>().NotNull().As<IRssFeedRepository>().NotNull().SingleInstance();
            builder.RegisterType<RssMessagesRepository>().NotNull().As<IRssMessagesRepository>().NotNull().SingleInstance();
            builder.RegisterType<ConfigurationRepository>().NotNull().As<IConfigurationRepository>().NotNull().SingleInstance();
            builder.RegisterType<FeedlyRepository>().NotNull().As<IFeedlyRepository>().NotNull().SingleInstance();

            builder.RegisterType<RssFeedMapper>().NotNull().As<IMapper<RssFeedModel, RssFeedDomainModel>>().NotNull().SingleInstance();
            builder.RegisterType<RssFeedMapper>().NotNull().As<IMapper<RssFeedDomainModel, RssFeedModel>>().NotNull().SingleInstance();
            builder.RegisterType<RssFeedMapper>().NotNull().As<IMapper<RssFeedDomainModel, RssFeedServiceModel>>().NotNull().SingleInstance();
            builder.RegisterType<RssFeedMapper>().NotNull().As<IMapper<RssFeedServiceModel, RssFeedDomainModel>>().NotNull().SingleInstance();

            builder.RegisterType<RssMessageMapper>().NotNull().As<IMapper<RssMessageModel, RssMessageDomainModel>>().NotNull().SingleInstance();
            builder.RegisterType<RssMessageMapper>().NotNull().As<IMapper<RssMessageDomainModel, RssMessageModel>>().NotNull().SingleInstance();
            builder.RegisterType<RssMessageMapper>().NotNull().As<IMapper<RssMessageServiceModel, RssMessageDomainModel>>().NotNull().SingleInstance();
            builder.RegisterType<RssMessageMapper>().NotNull().As<IMapper<RssMessageDomainModel, RssMessageServiceModel>>().NotNull().SingleInstance();

            builder.RegisterType<FeedlyMapper>().NotNull().As<IMapper<FeedlyRssApiModel, FeedlyRssDomainModel>>().NotNull().SingleInstance();
        }
    }
}
