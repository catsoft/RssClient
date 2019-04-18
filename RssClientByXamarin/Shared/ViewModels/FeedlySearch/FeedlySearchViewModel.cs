using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Droid.Repository.Configuration;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shared.Configuration.Settings;
using Shared.Infrastructure.ViewModels;
using Shared.Repository.Feedly;
using Shared.Repository.Rss;
using Shared.Services.Feedly;

namespace Shared.ViewModels.FeedlySearch
{
    public class FeedlySearchViewModel : ViewModel
    {
        private readonly IFeedlyService _feedlyService;
        private readonly IConfigurationRepository _configurationRepository;

        public FeedlySearchViewModel(IFeedlyService feedlyService, IConfigurationRepository configurationRepository)
        {
            _feedlyService = feedlyService;
            _configurationRepository = configurationRepository;

            AppConfiguration = _configurationRepository.GetSettings<AppConfiguration>();

            SourceList = new SourceList<FeedlyRssDomainModel>();
            ConnectChanges = SourceList.Connect();
            
            FindByQueryCommand = ReactiveCommand.CreateFromTask<string, IEnumerable<FeedlyRssDomainModel>>(
                (query, token) => _feedlyService.FindByQueryAsync(query ?? "", token),
                this.WhenAnyValue(model => model.SearchQuery).Select(w => !string.IsNullOrEmpty(w)));

            FindByQueryCommand.Subscribe(w =>
            {
                SourceList.Clear();
                SourceList.AddRange(w);
            });
            FindByQueryCommand.Select(w => w == null || !w.Any()).ToPropertyEx(this, model => model.IsEmpty, true);

            AddFeedlyRssCommand = ReactiveCommand.CreateFromTask<FeedlyRssDomainModel>(DoAddFeedlyRss);
            
            this.WhenAnyValue(vm => vm.SearchQuery)
                .Throttle(TimeSpan.FromSeconds(0.35f))
                .InvokeCommand(FindByQueryCommand);
        }

        public AppConfiguration AppConfiguration { get; }

        [Reactive]
        public string SearchQuery { get; set; }
        
        public ReactiveCommand<string, IEnumerable<FeedlyRssDomainModel>> FindByQueryCommand { get; }
        
        public ReactiveCommand<FeedlyRssDomainModel, Unit> AddFeedlyRssCommand { get; }
        
        public SourceList<FeedlyRssDomainModel> SourceList { get; }
        
        public IObservable<IChangeSet<FeedlyRssDomainModel>> ConnectChanges { get; }

        public extern bool IsEmpty { [ObservableAsProperty] get; }
        
        private Task DoAddFeedlyRss(FeedlyRssDomainModel model, CancellationToken token)
        {
            return _feedlyService.AddFeedly(model, token);
            //TODO го тоаст
//                Activity.Toast(Activity.GetText(Resource.String.recommended_rss_add_rss_toast) + viewHolder.Item.Title);
        }
    }
}