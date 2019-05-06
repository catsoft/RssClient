using Autofac;
using Autofac.Core;
using Core.Analytics;
using Core.Analytics.Rss;
using Core.Api.RssFeeds;
using Core.Container.Modules;
using Core.CoreServices.Html;
using Core.Database;
using Core.Extensions;
using Core.Infrastructure.Widgets;
using Core.Repositories.Configurations;
using Core.Repositories.RssFeeds;
using Core.Repositories.RssMessage;
using Core.Services.RssFeeds;
using JetBrains.Annotations;
using IContainer = Autofac.IContainer;

namespace Core
{
    public class App
    {
        public static IContainer Container { get; private set; }

        public static void BuildIfNever([CanBeNull] params IModule[] modules)
        {
            if (Container != null) return;
            
            var containerBuilder = new ContainerBuilder();

            foreach (var module in modules ?? new IModule[0])
                containerBuilder.RegisterModule(module);

            containerBuilder.RegisterModule(new AnalyticModule());
            containerBuilder.RegisterModule(new DatabaseModule());
            containerBuilder.RegisterModule(new RepositoryModule());
            containerBuilder.RegisterModule(new ApiModule());
            containerBuilder.RegisterModule(new CrossServicesModule());
            containerBuilder.RegisterModule(new ServicesModule());
            containerBuilder.RegisterModule(new ViewModelModule());
            containerBuilder.RegisterModule(new CoreServiceModule());

            Container = containerBuilder.Build().NotNull();

            FillDepending();
        }
    
        private static void FillDepending()
        {
            var rssFeedService = Container.Resolve<IRssFeedService>();
            var widgetUpdater = Container.Resolve<IRssListWidgetUpdater>();
            rssFeedService.CollectionChanged += (sender, args) => widgetUpdater.Update();
        }

        public static IRssFeedService BuildRssFeedService()
        {
            var log = new Log();
            var database = new SqliteDatabase(new Log());
            var mapper = new RssFeedMapper();
            var configRepo = new ConfigurationRepository(database);
            var messageMapper = new RssMessageMapper(new HtmlConfigurator(configRepo));
            var messageRep = new RssMessagesRepository(database, configRepo, messageMapper, messageMapper);
            var apiClient = new RssFeedApiClient(log);
            var feedRepo = new RssFeedRepository(database, new RssLog(log), mapper, mapper);
            return new RssFeedService(feedRepo, mapper, mapper, apiClient, messageRep, log);
        }
    }
}
