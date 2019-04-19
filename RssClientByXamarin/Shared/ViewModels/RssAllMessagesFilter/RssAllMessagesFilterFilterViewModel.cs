#region

using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Droid.EmbeddedResourse;
using Droid.Repositories.Configuration;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shared.Configuration.Settings;
using Shared.Infrastructure.Locale;
using Shared.Infrastructure.ViewModels;

#endregion

namespace Shared.ViewModels.RssAllMessagesFilter
{
    public class RssAllMessagesFilterFilterViewModel : ViewModel
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly ISubject<AllMessageFilterConfiguration> _messageFilter;

        public RssAllMessagesFilterFilterViewModel(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;

            _messageFilter = new Subject<AllMessageFilterConfiguration>();
            Filter = _messageFilter.AsObservable();

            Filter.Select(w => w.MessageFilterType)
                .ToPropertyEx(this, model => model.MessageFilterType);

            Filter.Select(w => w.From ?? DateTime.Now)
                .ToPropertyEx(this, model => model.FromDate);

            Filter.Select(w => w.To ?? DateTime.Now)
                .ToPropertyEx(this, model => model.ToDate);

            Filter.Select(w => w.From)
                .Subscribe(w => FromDateText = w?.ToShortDateLocaleString() ?? Strings.FilterFromTitle);

            Filter.Select(w => w.To)
                .Subscribe(w => ToDateText = w?.ToShortDateLocaleString() ?? Strings.FilterToTitle);

            Filter.ToPropertyEx(this, model => model.FilterConfiguration);

            SetMessageFilterTypeCommand = ReactiveCommand.Create<MessageFilterType>(DoSetMessageFilterType);
            SetFromDateTypeCommand = ReactiveCommand.Create<DateTime>(DoSetFromDate);
            SetToDateTypeCommand = ReactiveCommand.Create<DateTime>(DoSetToDate);

            _messageFilter.OnNext(_configurationRepository.GetSettings<AllMessageFilterConfiguration>());
        }

        [Reactive] public string FromDateText { get; set; }

        [Reactive] public string ToDateText { get; set; }

        public IObservable<AllMessageFilterConfiguration> Filter { get; }

        public extern AllMessageFilterConfiguration FilterConfiguration { [ObservableAsProperty] get; }

        public extern MessageFilterType MessageFilterType { [ObservableAsProperty] get; }

        public extern DateTime FromDate { [ObservableAsProperty] get; }

        public extern DateTime ToDate { [ObservableAsProperty] get; }

        public ReactiveCommand<MessageFilterType, Unit> SetMessageFilterTypeCommand { get; }

        public ReactiveCommand<DateTime, Unit> SetFromDateTypeCommand { get; }

        public ReactiveCommand<DateTime, Unit> SetToDateTypeCommand  { get; }

        private void DoSetMessageFilterType(MessageFilterType type) { UpdateFilter(filter => { filter.MessageFilterType = type; }); }

        private void DoSetFromDate(DateTime fromDate) { UpdateFilter(filter => filter.From = fromDate); }

        private void DoSetToDate(DateTime toDate) { UpdateFilter(filter => filter.To = toDate); }

        private void UpdateFilter(Action<AllMessageFilterConfiguration> update)
        {
            update?.Invoke(FilterConfiguration);
            _configurationRepository.SaveSetting(FilterConfiguration);
            _messageFilter.OnNext(FilterConfiguration);
        }
    }
}
