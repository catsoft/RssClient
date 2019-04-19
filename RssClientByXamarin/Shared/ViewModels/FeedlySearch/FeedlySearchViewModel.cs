#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Droid.Repositories.Configuration;
using DynamicData;
using JetBrains.Annotations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shared.Configuration.Settings;
using Shared.Extensions;
using Shared.Infrastructure.ViewModels;
using Shared.Repositories.Feedly;
using Shared.Services.Feedly;

#endregion

namespace Shared.ViewModels.FeedlySearch
{
    public class FeedlySearchViewModel : ViewModel
    {
        [NotNull] private readonly IFeedlyService _feedlyService;

        public FeedlySearchViewModel([NotNull] IFeedlyService feedlyService, [NotNull] IConfigurationRepository configurationRepository)
        {
            _feedlyService = feedlyService;

            AppConfiguration = configurationRepository.GetSettings<AppConfiguration>().NotNull();

            SourceList = new SourceList<FeedlyRssDomainModel>();
            ConnectChanges = SourceList.Connect().NotNull();

            FindByQueryCommand = ReactiveCommand.CreateFromTask<string, IEnumerable<FeedlyRssDomainModel>>((query, token) =>
                        _feedlyService.FindByQueryAsync(query ?? "", token),
                    this.WhenAnyValue(model => model.SearchQuery).NotNull().Select(w => !string.IsNullOrEmpty(w)))
                .NotNull();

            FindByQueryCommand.Subscribe(w =>
            {
                SourceList.Clear();
                SourceList.AddRange(w);
            });
            FindByQueryCommand.Select(w => w == null || !w.Any()).ToPropertyEx(this, model => model.IsEmpty, true);

            AddFeedlyRssCommand = ReactiveCommand.CreateFromTask<FeedlyRssDomainModel>(DoAddFeedlyRss).NotNull();

            this.WhenAnyValue(vm => vm.SearchQuery)
                .NotNull()
                .Throttle(TimeSpan.FromSeconds(0.35f))
                .InvokeCommand(FindByQueryCommand);
        }

        [NotNull] public AppConfiguration AppConfiguration { get; }

        [Reactive] [CanBeNull] public string SearchQuery { get; set; }

        [NotNull] public ReactiveCommand<string, IEnumerable<FeedlyRssDomainModel>> FindByQueryCommand { get; }

        [NotNull] public ReactiveCommand<FeedlyRssDomainModel, Unit> AddFeedlyRssCommand { get; }

        [NotNull] public SourceList<FeedlyRssDomainModel> SourceList { get; }

        [NotNull] public IObservable<IChangeSet<FeedlyRssDomainModel>> ConnectChanges { get; }

        public extern bool IsEmpty { [ObservableAsProperty] get; }

        [NotNull]
        private Task DoAddFeedlyRss([NotNull] FeedlyRssDomainModel model, CancellationToken token = default)
        {
            return _feedlyService.AddFeedly(model, token);

            //TODO го тоаст
//                Activity.Toast(Activity.GetText(Resource.String.recommended_rss_add_rss_toast) + viewHolder.Item.Title);
        }
    }
}
