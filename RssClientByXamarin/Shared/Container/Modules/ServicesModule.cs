using Autofac;
using Shared.Services;
using Shared.Services.Feedly;
using Shared.Services.Rss;

namespace Shared.Container.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<RssService>().As<IRssService>().SingleInstance();
            builder.RegisterType<FeedlyService>().As<IFeedlyService>().SingleInstance();
        }
    }
}