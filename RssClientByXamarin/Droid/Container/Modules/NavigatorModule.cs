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
using Droid.Screens.RssEditList;
using Droid.Screens.RssItemDetail;
using Droid.Screens.RssList;
using Droid.Screens.RssMessage;
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

            builder.RegisterType<CloseWay>().As<CloseViewModel.Way>();

            builder.RegisterType<RssItemDetailWay>().As<RssItemDetailViewModel.Way>();
            builder.RegisterType<RssAllMessagesListWay>().As<RssAllMessagesViewModel.Way>();
            builder.RegisterType<RssListWay>().As<RssListViewModel.Way>();
            builder.RegisterType<RssListEditWay>().As<RssListEditViewModel.Way>();
            builder.RegisterType<RssCreateWay>().As<RssCreateViewModel.Way>();
            builder.RegisterType<RssEditWay>().As<RssEditViewModel.Way>();
            builder.RegisterType<RecommendedCategoryListWay>().As<RecommendedCategoryListViewModel.Way>();
            builder.RegisterType<RecommendedRssListWay>().As<RecommendedViewModel.Way>();
            builder.RegisterType<RssMessageWay>().As<RssMessageViewModel.Way>();

            builder.RegisterType<AboutWay>().As<AboutViewModel.Way>();
            builder.RegisterType<SettingsWay>().As<SettingsViewModel.Way>();
            builder.RegisterType<ContactsWay>().As<ContactsViewModel.Way>();


            builder.Register((c) => MainActivity.Instance).AsSelf();
            builder.Register((c) => MainActivity.Instance).As<FragmentActivity>();
        }
    }
}