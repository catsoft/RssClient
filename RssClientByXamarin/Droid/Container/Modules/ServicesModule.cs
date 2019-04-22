using Autofac;
using Droid.Infrastructure.Dialogs;
using Droid.Infrastructure.Locale;
using Shared.Infrastructure.Dialogs;
using Shared.Infrastructure.Locale;

namespace Droid.Container.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<Locale>().As<ILocale>().SingleInstance();
            builder.RegisterType<DialogService>().As<IDialogService>().SingleInstance();
        }
    }
}
