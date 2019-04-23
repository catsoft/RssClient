using Autofac;
using Core.Analytics;
using Core.Analytics.Rss;
using Core.Extensions;

namespace Core.Container.Modules
{
    public class AnalyticModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<Log>().NotNull().As<ILog>().NotNull().SingleInstance();
            builder.RegisterType<ScreenLog>().AsSelf().NotNull().SingleInstance();
            builder.RegisterType<RssLog>().AsSelf().NotNull().SingleInstance();
            builder.RegisterType<RssMessageLog>().AsSelf().NotNull().SingleInstance();
        }
    }
}
