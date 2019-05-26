using Android.App;
using Android.Support.V7.App;
using Autofac;
using Core.Extensions;
using Core.Infrastructure.Navigation;
using Core.ViewModels.About;
using Core.ViewModels.AnimationWeaver;
using Core.ViewModels.Close;
using Core.ViewModels.Contacts;
using Core.ViewModels.Donate;
using Core.ViewModels.FeedlySearch;
using Core.ViewModels.Messages.AllMessages;
using Core.ViewModels.Messages.AllMessagesFilter;
using Core.ViewModels.Messages.Book;
using Core.ViewModels.Messages.FavoriteMessages;
using Core.ViewModels.Messages.Message;
using Core.ViewModels.Messages.RssFeedMessagesList;
using Core.ViewModels.RssFeeds.Create;
using Core.ViewModels.RssFeeds.Edit;
using Core.ViewModels.RssFeeds.EditableList;
using Core.ViewModels.RssFeeds.List;
using Core.ViewModels.Settings;
using Droid.Screens.About;
using Droid.Screens.AnimationWeaver;
using Droid.Screens.Close;
using Droid.Screens.Contacts;
using Droid.Screens.Donate;
using Droid.Screens.FeedlySearch;
using Droid.Screens.Main;
using Droid.Screens.Messages.AllMessages;
using Droid.Screens.Messages.AllMessagesFilter;
using Droid.Screens.Messages.Book;
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

            builder.RegisterType<CloseWay>().NotNull().As<IWay<CloseViewModel>>();

            builder.RegisterType<RssFeedMessagesListWay>().NotNull().As<IWayWithParameters<RssFeedMessagesListViewModel, RssFeedMessagesListParameters>>();
            builder.RegisterType<AllMessagesWay>().NotNull().As<IWay<AllMessagesViewModel>>();
            builder.RegisterType<FavoriteMessagesListWay>().NotNull().As<IWay<FavoriteMessagesViewModel>>();
            builder.RegisterType<AllMessagesFilterWay>().NotNull().As<IWay<AllMessagesFilterViewModel>>();
            builder.RegisterType<RssFeedListWay>().NotNull().As<IWay<RssFeedListViewModel>>();
            builder.RegisterType<RssFeedEditableListWay>().NotNull().As<IWay<RssFeedEditableListViewModel>>();
            builder.RegisterType<RssFeedCreateWay>().NotNull().As<IWay<RssFeedCreateViewModel>>();
            builder.RegisterType<RssFeedEditWay>().NotNull().As<IWayWithParameters<RssFeedEditViewModel, RssEditParameters>>();
            builder.RegisterType<MessageWay>().NotNull().As<IWayWithParameters<MessageViewModel, MessageParameters>>();
            builder.RegisterType<BookMessagesWay>().NotNull().As<IWayWithParameters<BookMessagesViewModel, BookMessagesParameter>>();

            builder.RegisterType<AboutWay>().NotNull().As<IWay<AboutViewModel>>();
            builder.RegisterType<SettingsWay>().NotNull().As<IWay<SettingsViewModel>>();
            builder.RegisterType<AnimationWeaverWay>().NotNull().As<IWay<AnimationWeaverViewModel>>();
            builder.RegisterType<ContactsWay>().NotNull().As<IWay<ContactsViewModel>>();
            builder.RegisterType<DonateWay>().NotNull().As<IWay<DonateViewModel>>();

            builder.RegisterType<FeedlySearchWay>().NotNull().As<IWay<FeedlySearchViewModel>>();
            
            builder.Register(c => MainActivity.Instance).NotNull().As<Activity>();
            builder.Register(c => MainActivity.Instance).NotNull().As<AppCompatActivity>();
            builder.Register(c => MainActivity.Instance).NotNull().As<IFragmentManager>();
        }
    }
}
