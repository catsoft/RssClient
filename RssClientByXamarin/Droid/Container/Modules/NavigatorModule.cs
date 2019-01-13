using Autofac;
using Droid.Infrastructure;
using Droid.Screens.Close;
using Droid.Screens.Main;
using Shared.Services.Navigator;
using Shared.ViewModels;

namespace Droid.Container.Modules
{
    public class NavigatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<CloseWay>().As<IWay<CloseViewModel, CloseViewModel.CloseWay.CloseWayData>>();
            builder.Register((c) => MainActivity.Instance).AsSelf();
        }
    }
}