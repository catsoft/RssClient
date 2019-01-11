using Autofac;
using Droid.Services.Locale;
using Shared.Services.Locale;

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