#region

using Autofac;
using Droid.Repositories.Configuration;
using Shared.Api.Feedly;
using Shared.Database.Rss;
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

            builder.RegisterType<RssRepository>().As<IRssRepository>().SingleInstance();
            builder.RegisterType<RssMessagesRepository>().As<IRssMessagesRepository>().SingleInstance();
            builder.RegisterType<ConfigurationRepository>().As<IConfigurationRepository>().SingleInstance();
            builder.RegisterType<FeedlyRepository>().As<IFeedlyRepository>().SingleInstance();

            builder.RegisterType<RssMapper>().As<IMapper<RssModel, RssDomainModel>>().SingleInstance();
            builder.RegisterType<RssMapper>().As<IMapper<RssDomainModel, RssModel>>().SingleInstance();
            builder.RegisterType<RssMapper>().As<IMapper<RssDomainModel, RssServiceModel>>().SingleInstance();
            builder.RegisterType<RssMapper>().As<IMapper<RssServiceModel, RssDomainModel>>().SingleInstance();

            builder.RegisterType<RssMessageMapper>().As<IMapper<RssMessageModel, RssMessageDomainModel>>().SingleInstance();
            builder.RegisterType<RssMessageMapper>().As<IMapper<RssMessageDomainModel, RssMessageModel>>().SingleInstance();

            builder.RegisterType<FeedlyMapper>().As<IMapper<FeedlyRssApiModel, FeedlyRssDomainModel>>().SingleInstance();
        }
    }
}
