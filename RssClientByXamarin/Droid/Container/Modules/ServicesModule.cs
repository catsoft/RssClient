using Autofac;
using Droid.Infrastructure.Locale;
using Shared.Infrastructure.Locale;

namespace Droid.Container.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<Locale>().As<ILocale>().SingleInstance();
        }
    }
}