using Android.App;
using Autofac;
using Droid.Screens.About;
using Droid.Screens.Close;
using Droid.Screens.Contacts;
using Droid.Screens.FeedlySearch;
using Droid.Screens.Main;
using Droid.Screens.Navigation;
using Droid.Screens.RssAllMessages;
using Droid.Screens.RssAllMessagesFilter;
using Droid.Screens.RssCreate;
using Droid.Screens.RssEdit;
using Droid.Screens.RssEditList;
using Droid.Screens.RssFavoriteMessagesList;
using Droid.Screens.RssItemMessage;
using Droid.Screens.RssList;
using Droid.Screens.RssMessage;
using Droid.Screens.Settings;
using Shared.Infrastructure.Navigation;
using Shared.ViewModels.About;
using Shared.ViewModels.Close;
using Shared.ViewModels.Contacts;
using Shared.ViewModels.FeedlySearch;
using Shared.ViewModels.RssAllMessages;
using Shared.ViewModels.RssAllMessagesFilter;
using Shared.ViewModels.RssCreate;
using Shared.ViewModels.RssEdit;
using Shared.ViewModels.RssFavoriteMessages;
using Shared.ViewModels.RssItemDetail;
using Shared.ViewModels.RssList;
using Shared.ViewModels.RssListEdit;
using Shared.ViewModels.RssMessage;
using Shared.ViewModels.Settings;

namespace Droid.Container.Modules
{
    public class NavigationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<CloseWay>().As<IWay<CloseViewModel>>();

            builder.RegisterType<RssItemDetailWay>().As<IWayWithParameters<RssItemDetailViewModel, RssItemDetailParameterses>>();
            builder.RegisterType<RssAllMessagesWay>().As<IWay<RssAllMessagesViewModel>>();
            builder.RegisterType<RssFavoriteMessagesListWay>().As<IWay<RssFavoriteMessagesViewModel>>();
            builder.RegisterType<RssAllMessagesFilterWay>().As<IWay<RssAllMessagesFilterViewModel>>();
            builder.RegisterType<RssListWay>().As<IWay<RssListViewModel>>();
            builder.RegisterType<RssListEditWay>().As<IWay<RssListEditViewModel>>();
            builder.RegisterType<RssCreateWay>().As<IWay<RssCreateViewModel>>();
            builder.RegisterType<RssEditWay>().As<IWayWithParameters<RssEditViewModel, RssEditParameterses>>();
            builder.RegisterType<RssMessageWay>().As<IWayWithParameters<RssMessageViewModel, RssMessageParameterses>>();

            builder.RegisterType<AboutWay>().As<IWay<AboutViewModel>>();
            builder.RegisterType<SettingsWay>().As<IWay<SettingsViewModel>>();
            builder.RegisterType<ContactsWay>().As<IWay<ContactsViewModel>>();

            builder.RegisterType<FeedlySearchWay>().As<IWay<FeedlySearchViewModel>>();
            
            builder.Register((c) => MainActivity.Instance).As<Activity>();
            builder.Register((c) => MainActivity.Instance).As<FragmentActivity>();
        }
    }
}