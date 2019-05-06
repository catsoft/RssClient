using Autofac;
using Droid.Widget.RssList;

namespace Droid.Container.Modules
{
    public class PlatformModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule(new ServicesModule());
            builder.RegisterModule(new NavigationModule());
            builder.RegisterModule(new WidgetModule());
        }
    }
}
