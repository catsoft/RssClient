using Analytics;
using Analytics.Rss;
using Api;
using Autofac;
using Database;
using Repository;

namespace Shared
{
    public class App
    {
        public static IContainer Container { get; private set; }

        static App()
        {
            Build(new ContainerBuilder());
        }

        public static void Build(ContainerBuilder builder)
        {
            builder.Register(c => new RealmDatabase()).AsSelf().SingleInstance();
            builder.RegisterType<RssRepository>().As<IRssRepository>().SingleInstance();
            builder.RegisterType<RssMessagesRepository>().As<IRssMessagesRepository>().SingleInstance();

            builder.RegisterType<Log>().As<ILog>().SingleInstance();
            builder.RegisterType<RssLog>().AsSelf().SingleInstance();
            builder.RegisterType<RssMessageLog>().AsSelf().SingleInstance();
            builder.RegisterType<ScreenLog>().AsSelf().SingleInstance();

            builder.RegisterType<RssApiClient>().As<IRssApiClient>().SingleInstance();

            Container = builder.Build();
        }
    }
}
