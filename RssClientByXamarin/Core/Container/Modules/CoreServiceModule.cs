using Autofac;
using Core.CoreServices.Html;

namespace Core.Container.Modules
{
    public class CoreServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<HtmlConfigurator>().As<IHtmlConfigurator>().SingleInstance();
        }
    }
}