using Autofac;
using Autofac.Core;
using Shared.Infrastructure.ViewModels;
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

namespace Shared.Container.Modules
{
    public class ViewModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ViewModelProvider>().AsSelf().SingleInstance();

            builder.RegisterType<AboutViewModel>().AsSelf().InstancePerRequest();
            builder.RegisterType<CloseViewModel>().AsSelf().InstancePerRequest();
            builder.RegisterType<ContactsViewModel>().AsSelf().InstancePerRequest();
            builder.RegisterType<FeedlySearchViewModel>().AsSelf().InstancePerRequest();
            builder.RegisterType<RssAllMessagesViewModel>().AsSelf().InstancePerRequest();
            builder.RegisterType<RssAllMessagesFilterViewModel>().AsSelf().InstancePerRequest();
            builder.RegisterType<RssCreateViewModel>().AsSelf().InstancePerRequest();
            builder.RegisterType<RssEditViewModel>().AsSelf().InstancePerRequest();
            builder.RegisterType<RssFavoriteMessagesViewModel>().AsSelf().InstancePerRequest();
            builder.RegisterType<RssItemDetailViewModel>().AsSelf().InstancePerRequest();
            builder.RegisterType<RssListViewModel>().AsSelf().InstancePerRequest();
            builder.RegisterType<RssListEditViewModel>().AsSelf().InstancePerRequest();
            builder.RegisterType<RssMessageViewModel>().AsSelf().InstancePerRequest();
            builder.RegisterType<SettingsViewModel>().AsSelf().InstancePerRequest();
        }
    }
}