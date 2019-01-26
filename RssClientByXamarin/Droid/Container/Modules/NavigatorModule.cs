using Autofac;
using Droid.Screens.About;
using Droid.Screens.Close;
using Droid.Screens.Contacts;
using Droid.Screens.Main;
using Droid.Screens.Navigation;
using Droid.Screens.RecommendedCategoryList;
using Droid.Screens.RecommendedRssList;
using Droid.Screens.RssAllMessagesList;
using Droid.Screens.RssCreate;
using Droid.Screens.RssEdit;
using Droid.Screens.RssItemDetail;
using Droid.Screens.RssList;
using Droid.Screens.Settings;
using Shared.Services.Navigator;
using Shared.ViewModels;

namespace Droid.Container.Modules
{
    public class NavigatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<CloseWay>().As<IWay<CloseViewModel, CloseViewModel.Way.WayData>>();
            
            builder.RegisterType<RssItemDetailWay>().As<IWay<RssItemDetailViewModel, RssItemDetailViewModel.Way.WayData>>();
            builder.RegisterType<RssAllMessagesListWay>().As<IWay<RssAllMessagesViewModel, RssAllMessagesViewModel.Way.WayData>>();
            builder.RegisterType<RssListWay>().As<IWay<RssListViewModel, RssListViewModel.Way.WayData>>();
            builder.RegisterType<RssCreateWay>().As<IWay<RssCreateViewModel, RssCreateViewModel.Way.WayData>>();
            builder.RegisterType<RssEditWay>().As<IWay<RssEditViewModel, RssEditViewModel.Way.WayData>>();
            builder.RegisterType<RecommendedCategoryListWay>().As<IWay<RecommendedCategoryListViewModel, RecommendedCategoryListViewModel.Way.WayData>>();
            builder.RegisterType<RecommendedRssListWay>().As<IWay<RecommendedViewModel, RecommendedViewModel.Way.WayData>>();
            
            builder.RegisterType<AboutWay>().As<IWay<AboutViewModel, AboutViewModel.Way.WayData>>();
            builder.RegisterType<SettingsWay>().As<IWay<SettingsViewModel, SettingsViewModel.Way.WayData>>();
            builder.RegisterType<ContactsWay>().As<IWay<ContactsViewModel, ContactsViewModel.Way.WayData>>();

            
            builder.Register((c) => MainActivity.Instance).AsSelf();
            builder.Register((c) => MainActivity.Instance).As<FragmentActivity>();
        }
    }
}