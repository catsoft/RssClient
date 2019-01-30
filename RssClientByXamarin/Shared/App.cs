using Autofac;
using Autofac.Core;
using Shared.Container.Modules;

namespace Shared
{
    public class App
    {
        public static IContainer Container { get; private set; }

        public static void Build(params IModule[] modules)
        {
            var containerBuilder = new ContainerBuilder();

            foreach (var module in modules)
                containerBuilder.RegisterModule(module);

            containerBuilder.RegisterModule(new AnalitycModule());
            containerBuilder.RegisterModule(new DatabaseModule());
            containerBuilder.RegisterModule(new RepositoryModule());
            containerBuilder.RegisterModule(new ApiModule());
            containerBuilder.RegisterModule(new CrossServicesModule());

            Container = containerBuilder.Build();
        }
    }
}