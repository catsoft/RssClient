using Autofac;
using Core.Extensions;
using Core.Infrastructure.ViewModels;
using Core.ViewModels.About;
using Core.ViewModels.AnimationWeaver;
using Core.ViewModels.Close;
using Core.ViewModels.Contacts;
using Core.ViewModels.Donate;
using Core.ViewModels.FeedlySearch;
using Core.ViewModels.Main;
using Core.ViewModels.Messages.AllMessages;
using Core.ViewModels.Messages.AllMessagesFilter;
using Core.ViewModels.Messages.FavoriteMessages;
using Core.ViewModels.Messages.Message;
using Core.ViewModels.Messages.RssFeedMessagesList;
using Core.ViewModels.RssFeeds.Create;
using Core.ViewModels.RssFeeds.Edit;
using Core.ViewModels.RssFeeds.EditableList;
using Core.ViewModels.RssFeeds.List;
using Core.ViewModels.RssFeeds.RssFeedsUpdater;
using Core.ViewModels.Settings;
using Core.ViewModels.Settings.Animations;
using Core.ViewModels.Settings.AutoUpdating;
using Core.ViewModels.Settings.HideReadMessages;
using Core.ViewModels.Settings.LoadImages;
using Core.ViewModels.Settings.Pushes;
using Core.ViewModels.Settings.ReaderTypes;
using Core.ViewModels.Settings.RssDetail;
using Core.ViewModels.Settings.StartPage;
using Core.ViewModels.Settings.Theme;

namespace Core.Container.Modules
{
    public class ViewModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ViewModelProvider>().NotNull().AsSelf().NotNull().SingleInstance();

            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<CloseViewModel>().AsSelf();
            
            builder.RegisterType<FavoriteMessagesViewModel>().AsSelf();
            builder.RegisterType<FeedlySearchViewModel>().AsSelf();
            builder.RegisterType<SettingsViewModel>().AsSelf();
            builder.RegisterType<ContactsViewModel>().AsSelf();
            builder.RegisterType<DonateViewModel>().AsSelf();
            builder.RegisterType<AboutViewModel>().AsSelf();
    
            builder.RegisterType<AllMessagesFilterFilterViewModel>().AsSelf();
            builder.RegisterType<AllMessagesOrderFilterViewModel>().AsSelf();
            builder.RegisterType<AllMessagesViewModel>().AsSelf();
            builder.RegisterType<AllMessagesFilterViewModel>().AsSelf();
            
            builder.RegisterType<RssFeedCreateViewModel>().AsSelf();
            builder.RegisterType<RssFeedEditViewModel>().AsSelf();
            builder.RegisterType<RssFeedMessagesListViewModel>().AsSelf();
            builder.RegisterType<RssFeedListViewModel>().AsSelf();
            builder.RegisterType<RssFeedEditableListViewModel>().AsSelf();
            builder.RegisterType<MessageViewModel>().AsSelf();

            builder.RegisterType<SettingsAnimationViewModel>().AsSelf();
            builder.RegisterType<SettingsLoadImagesViewModel>().AsSelf();
            builder.RegisterType<SettingsHideReadMessagesViewModel>().AsSelf();
            builder.RegisterType<SettingsRssDetailViewModel>().AsSelf();
            builder.RegisterType<SettingsStartPageViewModel>().AsSelf();
            builder.RegisterType<SettingsThemeViewModel>().AsSelf();
            builder.RegisterType<SettingsPushesViewModel>().AsSelf();
            builder.RegisterType<SettingsAutoUpdateViewModel>().AsSelf();
            builder.RegisterType<SettingsReaderTypeViewModel>().AsSelf();
            
            builder.RegisterType<AnimationWeaverViewModel>().AsSelf();
            
            builder.RegisterType<RssFeedsUpdaterViewModel>().AsSelf().SingleInstance();
        }
    }
}
