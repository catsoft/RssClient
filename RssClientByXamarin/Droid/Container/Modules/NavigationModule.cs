using Android.App;
using Autofac;
using Core.Infrastructure.Navigation;
using Core.ViewModels.About;
using Core.ViewModels.Close;
using Core.ViewModels.Contacts;
using Core.ViewModels.FeedlySearch;
using Core.ViewModels.Messages.AllMessages;
using Core.ViewModels.Messages.AllMessagesFilter;
using Core.ViewModels.Messages.FavoriteMessages;
using Core.ViewModels.Messages.Message;
using Core.ViewModels.Messages.RssFeedMessagesList;
using Core.ViewModels.RssFeeds.Create;
using Core.ViewModels.RssFeeds.Edit;
using Core.ViewModels.RssFeeds.EditableList;
using Core.ViewModels.RssFeeds.List;
using Core.ViewModels.Settings;
using Droid.Screens.About;
using Droid.Screens.Close;
using Droid.Screens.Contacts;
using Droid.Screens.FeedlySearch;
using Droid.Screens.Main;
using Droid.Screens.Messages.AllMessages;
using Droid.Screens.Messages.AllMessagesFilter;
using Droid.Screens.Messages.FavoriteMessagesList;
using Droid.Screens.Messages.Message;
using Droid.Screens.Messages.RssFeedMessagesList;
using Droid.Screens.Navigation;
using Droid.Screens.RssFeeds.Create;
using Droid.Screens.RssFeeds.Edit;
using Droid.Screens.RssFeeds.EditableList;
using Droid.Screens.RssFeeds.List;
using Droid.Screens.Settings;

namespace Droid.Container.Modules
{
    public class NavigationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<CloseWay>().As<IWay<CloseViewModel>>();

            builder.RegisterType<RssFeedMessagesListWay>().As<IWayWithParameters<RssFeedMessagesListViewModel, RssFeedMessagesListParameters>>();
            builder.RegisterType<AllMessagesWay>().As<IWay<AllMessagesViewModel>>();
            builder.RegisterType<FavoriteMessagesListWay>().As<IWay<FavoriteMessagesViewModel>>();
            builder.RegisterType<AllMessagesFilterWay>().As<IWay<AllMessagesFilterViewModel>>();
            builder.RegisterType<RssFeedListWay>().As<IWay<RssFeedListViewModel>>();
            builder.RegisterType<RssFeedEditableListWay>().As<IWay<RssFeedEditableListViewModel>>();
            builder.RegisterType<RssFeedCreateWay>().As<IWay<RssFeedCreateViewModel>>();
            builder.RegisterType<RssFeedEditWay>().As<IWayWithParameters<RssFeedEditViewModel, RssEditParameters>>();
            builder.RegisterType<MessageWay>().As<IWayWithParameters<MessageViewModel, MessageParameters>>();

            builder.RegisterType<AboutWay>().As<IWay<AboutViewModel>>();
            builder.RegisterType<SettingsWay>().As<IWay<SettingsViewModel>>();
            builder.RegisterType<ContactsWay>().As<IWay<ContactsViewModel>>();

            builder.RegisterType<FeedlySearchWay>().As<IWay<FeedlySearchViewModel>>();

            builder.Register(c => MainActivity.Instance).As<Activity>();
            builder.Register(c => MainActivity.Instance).As<IFragmentManager>();
        }
    }
}
