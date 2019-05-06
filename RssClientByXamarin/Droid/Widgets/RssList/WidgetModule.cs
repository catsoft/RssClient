using Autofac;
using Core.Infrastructure.Widgets;

namespace Droid.Widgets.RssList
{
    public class WidgetModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<RssListWidgetUpdater>().As<IRssListWidgetUpdater>().SingleInstance();
        }
    }
}