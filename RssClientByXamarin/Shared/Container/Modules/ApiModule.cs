using Autofac;
using Shared.Api;
using Shared.Api.Feedly;
using Shared.Api.Rss;

namespace Shared.Container.Modules
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<RssApiClient>().As<IRssApiClient>().SingleInstance();
            builder.RegisterType<FeedlyCloudApiClient>().As<IFeedlyCloudApiClient>().SingleInstance();
        }
    }
}