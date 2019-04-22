using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Droid.Repositories.Configuration;
using JetBrains.Annotations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shared.Configuration.Settings;
using Shared.Extensions;
using Shared.Infrastructure.ViewModels;

namespace Shared.ViewModels.RssAllMessagesFilter
{
    public class RssAllMessagesOrderFilterViewModel : ViewModel
    {
        [NotNull] private readonly IConfigurationRepository _configurationRepository;
        [NotNull] private readonly ISubject<AllMessageFilterConfiguration> _sortFilter;

        public RssAllMessagesOrderFilterViewModel([NotNull] IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;

            _sortFilter = new Subject<AllMessageFilterConfiguration>();
            Filter = _sortFilter.AsObservable();

            Filter.Select(filter => filter.NotNull().Sort)
                .ToPropertyEx(this, model => model.Sort);

            Filter.ToPropertyEx(this, model => model.FilterConfiguration);

            UpdateSortCommand = ReactiveCommand.Create<Sort>(DoUpdateSort).NotNull();
            _sortFilter.OnNext(_configurationRepository.GetSettings<AllMessageFilterConfiguration>());
        }

        [NotNull] public IObservable<AllMessageFilterConfiguration> Filter { get; }

        public extern Sort Sort { [ObservableAsProperty] get; }

        [NotNull] public extern AllMessageFilterConfiguration FilterConfiguration { [ObservableAsProperty] get; }

        [NotNull] public ReactiveCommand<Sort, Unit> UpdateSortCommand { get; }

        private void DoUpdateSort(Sort sort)
        {
            var config = FilterConfiguration;
            config.Sort = sort;
            _configurationRepository.SaveSetting(config);
            _sortFilter.OnNext(config);
        }
    }
}
