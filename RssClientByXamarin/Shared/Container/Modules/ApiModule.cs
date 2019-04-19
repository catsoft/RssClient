#region

using Autofac;
using Shared.Api.Feedly;
using Shared.Api.Rss;
using Shared.Extensions;

#endregion

namespace Shared.Container.Modules
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
