using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Droid.Repository.Configuration;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shared.Configuration.Settings;
using Shared.Infrastructure.ViewModels;

namespace Shared.ViewModels.RssAllMessagesFilter
{
    public class RssAllMessagesOrderFilterViewModel : ViewModel
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly ISubject<AllMessageFilterConfiguration> _sortFilter;

        public RssAllMessagesOrderFilterViewModel(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
            
            _sortFilter = new Subject<AllMessageFilterConfiguration>();
            Filter = _sortFilter.AsObservable();

            Filter.Select(w => w.Sort)
                .ToPropertyEx(this, model => model.Sort);

            Filter.ToPropertyEx(this, model => model.FilterConfiguration);
            
            UpdateSortCommand = ReactiveCommand.Create<Sort>(DoUpdateSort);
            _sortFilter.OnNext(_configurationRepository.GetSettings<AllMessageFilterConfiguration>());
        }

        public IObservable<AllMessageFilterConfiguration> Filter { get; }
        
        public extern Sort Sort { [ObservableAsProperty] get; }

        public extern AllMessageFilterConfiguration FilterConfiguration { [ObservableAsProperty] get; }
        
        public ReactiveCommand<Sort, Unit> UpdateSortCommand { get; }
        
        private void DoUpdateSort(Sort sort)
        {
            var config = FilterConfiguration;
            config.Sort = sort;
            _configurationRepository.SaveSetting(config);
            _sortFilter.OnNext(config);
        }
    }
}