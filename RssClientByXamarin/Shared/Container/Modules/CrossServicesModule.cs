using Autofac;
using Droid.Infrastructure;
using Shared.Services.Navigator;

namespace Shared.Container.Modules
{
    public class CrossServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<Navigator>().As<INavigator>().SingleInstance();
        }
    }
}
