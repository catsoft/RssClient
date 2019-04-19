#region

using Autofac;
using Shared.Analitics.Rss;
using Shared.Analytics;
using Shared.Analytics.Rss;

#endregion

namespace Shared.Container.Modules
{
    public class AnalitycModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<Log>().As<ILog>().SingleInstance();
            builder.RegisterType<ScreenLog>().AsSelf().SingleInstance();
            builder.RegisterType<RssLog>().AsSelf().SingleInstance();
            builder.RegisterType<RssMessageLog>().AsSelf().SingleInstance();
        }
    }
}
