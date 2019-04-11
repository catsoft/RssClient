using Android.App;
using Autofac;
using Droid.Screens.About;
using Droid.Screens.Close;
using Droid.Screens.Contacts;
using Droid.Screens.FeedlySearch;
using Droid.Screens.Main;
using Droid.Screens.Navigation;
using Droid.Screens.RssAllMessagesFilter;
using Droid.Screens.RssAllMessagesList;
using Droid.Screens.RssCreate;
using Droid.Screens.RssEdit;
using Droid.Screens.RssEditList;
using Droid.Screens.RssFavoriteMessagesList;
using Droid.Screens.RssItemMessage;
using Droid.Screens.RssList;
using Droid.Screens.RssMessage;
using Droid.Screens.Settings;
using Shared.ViewModels;

namespace Droid.Container.Modules
{
    public class NavigationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<CloseWay>().As<CloseViewModel.Way>();

            builder.RegisterType<RssItemDetailWay>().As<RssItemDetailViewModel.Way>();
            builder.RegisterType<RssAllMessagesListWay>().As<RssAllMessagesViewModel.Way>();
            builder.RegisterType<RssFavoriteMessagesListWay>().As<RssFavoriteMessagesViewModel.Way>();
            builder.RegisterType<RssAllMessagesFilterWay>().As<RssAllMessagesFilterViewModel.Way>();
            builder.RegisterType<RssListWay>().As<RssListViewModel.Way>();
            builder.RegisterType<RssListEditWay>().As<RssListEditViewModel.Way>();
            builder.RegisterType<RssCreateWay>().As<RssCreateViewModel.Way>();
            builder.RegisterType<RssEditWay>().As<RssEditViewModel.Way>();
            builder.RegisterType<RssMessageWay>().As<RssMessageViewModel.Way>();

            builder.RegisterType<AboutWay>().As<AboutViewModel.Way>();
            builder.RegisterType<SettingsWay>().As<SettingsViewModel.Way>();
            builder.RegisterType<ContactsWay>().As<ContactsViewModel.Way>();

            builder.RegisterType<FeedlySearchWay>().As<FeedlySearchViewModel.Way>();
            
            builder.Register((c) => MainActivity.Instance).As<Activity>();
            builder.Register((c) => MainActivity.Instance).As<FragmentActivity>();
        }
    }
}