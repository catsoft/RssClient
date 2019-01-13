using Autofac;
using Droid.Infrastructure;
using Droid.Screens.Close;
using Droid.Screens.Main;
using Droid.Screens.Navigation;
using Droid.Screens.RssItemDetail;
using Shared.ViewModels;

namespace Droid.Container.Modules
{
    public class NavigatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<CloseWay>().As<IWay<CloseViewModel, CloseViewModel.Way.WayData>>();
            builder.RegisterType<Way>().As<IWay<RssItemDetailViewModel, RssItemDetailViewModel.Way.DataModel>>();
            builder.Register((c) => MainActivity.Instance).AsSelf();
            builder.Register((c) => MainActivity.Instance).As<FragmentActivity>();
        }
    }
}