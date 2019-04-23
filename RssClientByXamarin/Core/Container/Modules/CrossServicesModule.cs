using Autofac;
using Core.Extensions;
using Core.Infrastructure.Navigation;

namespace Core.Container.Modules
{
    public class CrossServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<Navigator>().NotNull().As<INavigator>().NotNull().SingleInstance();
        }
    }
}
