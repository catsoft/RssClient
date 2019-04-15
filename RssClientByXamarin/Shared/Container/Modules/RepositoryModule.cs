using Autofac;
using Droid.Repository.Configuration;
using Shared.Database.Rss;
using Shared.Repository;
using Shared.Repository.Feedly;
using Shared.Repository.Rss;
using Shared.Repository.RssMessage;

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
            
            builder.RegisterType<RssMapper>().As<IMapper<RssModel, RssData>>().SingleInstance();
            builder.RegisterType<RssMessageMapper>().As<IMapper<RssMessageModel, RssMessageData>>().SingleInstance();
            builder.RegisterType<FeedlyMapper>().As<IMapper<Api.Feedly.FeedlyRss, FeedlyRss>>().SingleInstance();
        }
    }
}
