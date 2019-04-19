#region

using Autofac;
using Shared.Extensions;
using Shared.Infrastructure.ViewModels;
using Shared.ViewModels.About;
using Shared.ViewModels.Close;
using Shared.ViewModels.Contacts;
using Shared.ViewModels.FeedlySearch;
using Shared.ViewModels.Main;
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

#endregion

namespace Shared.Container.Modules
{
    public class ViewModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ViewModelProvider>().NotNull().AsSelf().NotNull().SingleInstance();

            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<AboutViewModel>().AsSelf();
            builder.RegisterType<CloseViewModel>().AsSelf();
            builder.RegisterType<ContactsViewModel>().AsSelf();
            builder.RegisterType<FeedlySearchViewModel>().AsSelf();
            builder.RegisterType<RssAllMessagesViewModel>().AsSelf();
            builder.RegisterType<RssAllMessagesFilterViewModel>().AsSelf();

            builder.RegisterType<RssAllMessagesFilterFilterViewModel>().AsSelf();
            builder.RegisterType<RssAllMessagesOrderFilterViewModel>().AsSelf();

            builder.RegisterType<RssCreateViewModel>().AsSelf();
            builder.RegisterType<RssEditViewModel>().AsSelf();
            builder.RegisterType<RssFavoriteMessagesViewModel>().AsSelf();
            builder.RegisterType<RssMessagesListViewModel>().AsSelf();
            builder.RegisterType<RssListViewModel>().AsSelf();
            builder.RegisterType<RssListEditViewModel>().AsSelf();
            builder.RegisterType<RssMessageViewModel>().AsSelf();
            builder.RegisterType<SettingsViewModel>().AsSelf();

            builder.RegisterType<SettingsAnimationViewModel>().AsSelf();
            builder.RegisterType<SettingsLoadImagesViewModel>().AsSelf();
            builder.RegisterType<SettingsReadMessagesViewModel>().AsSelf();
            builder.RegisterType<SettingsRssDetailViewModel>().AsSelf();
            builder.RegisterType<SettingsStartPageViewModel>().AsSelf();
            builder.RegisterType<SettingsThemeViewModel>().AsSelf();
        }
    }
}
