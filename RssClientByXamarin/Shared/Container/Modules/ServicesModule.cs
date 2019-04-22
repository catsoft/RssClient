using Autofac;
using Shared.Database.Rss;
using Shared.Extensions;
using Shared.Services.Feedly;
using Shared.Services.Rss;

namespace Shared.Container.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<RssService>().NotNull().As<IRssService>().NotNull().SingleInstance();
            builder.RegisterType<RssMessageService>().NotNull().As<IRssMessageService>().NotNull().SingleInstance();
            builder.RegisterType<FeedlyService>().NotNull().As<IFeedlyService>().NotNull().SingleInstance();
        }
    }
}
