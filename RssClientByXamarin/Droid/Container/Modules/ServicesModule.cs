using Autofac;
using Core.Infrastructure.Dialogs;
using Core.Infrastructure.Locale;
using Droid.Infrastructure.Dialogs;
using Droid.Infrastructure.Locale;

namespace Droid.Container.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<Locale>().As<ILocale>().SingleInstance();
            builder.RegisterType<DialogService>().As<IDialogService>().SingleInstance();
            builder.RegisterType<ToastService>().As<IToastService>().SingleInstance();
        }
    }
}
