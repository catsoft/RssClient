#region

using Autofac;
using Shared.Extensions;
using Shared.Infrastructure.Navigation;

#endregion

namespace Shared.Container.Modules
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
