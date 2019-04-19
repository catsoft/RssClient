#region

using Autofac;
using Shared.Services.Feedly;
using Shared.Services.Rss;

#endregion

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
