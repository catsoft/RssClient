using Autofac;
using Shared.Api;

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