using Autofac;

namespace iOS.Container.Modules
{
    public class PlatformModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule(new ServicesModule());
        }
    }
}