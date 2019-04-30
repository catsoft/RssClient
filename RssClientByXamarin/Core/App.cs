using Autofac;
using Autofac.Core;
using Core.Container.Modules;
using Core.Extensions;
using JetBrains.Annotations;
using IContainer = Autofac.IContainer;

namespace Core
{
    public class App
    {
        [NotNull]
        // ReSharper disable once NotNullMemberIsNotInitialized
        public static IContainer Container { get; private set; }

        public static void Build([CanBeNull] params IModule[] modules)
        {
            var containerBuilder = new ContainerBuilder();

            foreach (var module in modules ?? new IModule[0])
                containerBuilder.RegisterModule(module);

            containerBuilder.RegisterModule(new AnalyticModule());
            containerBuilder.RegisterModule(new DatabaseModule());
            containerBuilder.RegisterModule(new RepositoryModule());
            containerBuilder.RegisterModule(new ApiModule());
            containerBuilder.RegisterModule(new CrossServicesModule());
            containerBuilder.RegisterModule(new ServicesModule());
            containerBuilder.RegisterModule(new ViewModelModule());
            containerBuilder.RegisterModule(new CoreServiceModule());

            Container = containerBuilder.Build().NotNull();
        }
    }
}
