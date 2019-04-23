using Autofac;
using Core.Infrastructure.Locale;
using iOS.Services.Locale;

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