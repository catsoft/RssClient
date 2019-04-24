using Autofac;
using Core.Extensions;
using Core.Services.Feedly;
using Core.Services.RssFeeds;
using Core.Services.RssMessages;

namespace Core.Container.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<RssFeedService>().NotNull().As<IRssFeedService>().NotNull().SingleInstance();
            builder.RegisterType<RssMessageService>().NotNull().As<IRssMessageService>().NotNull().SingleInstance();
            builder.RegisterType<FeedlyService>().NotNull().As<IFeedlyService>().NotNull().SingleInstance();
        }
    }
}
