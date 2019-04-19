#region

using Autofac;
using Droid.Repositories.Configuration;
using Shared.Api.Feedly;
using Shared.Database.Rss;
using Shared.Extensions;
using Shared.Infrastructure.Mappers;
using Shared.Repositories.Feedly;
using Shared.Repositories.Rss;
using Shared.Repositories.RssMessage;
using Shared.Services.Rss;

#endregion

namespace Shared.Container.Modules
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

            builder.RegisterType<FeedlyMapper>().NotNull().As<IMapper<FeedlyRssApiModel, FeedlyRssDomainModel>>().NotNull().SingleInstance();
        }
    }
}
