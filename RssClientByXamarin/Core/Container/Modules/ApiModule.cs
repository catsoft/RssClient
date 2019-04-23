using Autofac;
using Core.Api.Feedly;
using Core.Api.Rss;
using Core.Extensions;

namespace Core.Container.Modules
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<RssApiClient>().NotNull().As<IRssApiClient>().NotNull().SingleInstance();
            builder.RegisterType<FeedlyCloudApiClient>().NotNull().As<IFeedlyCloudApiClient>().NotNull().SingleInstance();
        }
    }
}
