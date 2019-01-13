using Autofac;
using iOS.Services.Locale;
using Shared.Services.Locale;
using Shared.Services.Navigator;

namespace iOS.Container.Modules
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