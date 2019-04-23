using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Core.Configuration.AllMessageFilter;
using Core.Extensions;
using Core.Infrastructure.Locale;
using Core.Infrastructure.ViewModels;
using Core.Repositories.Configuration;
using Droid.EmbeddedResourse;
using JetBrains.Annotations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Core.ViewModels.Messages.AllMessagesFilter
{
    public class AllMessagesFilterFilterViewModel : ViewModel
    {
        [NotNull] private readonly IConfigurationRepository _configurationRepository;
        [NotNull] private readonly ISubject<AllMessageFilterConfiguration> _messageFilter;

        public AllMessagesFilterFilterViewModel([NotNull] IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;

            _messageFilter = new Subject<AllMessageFilterConfiguration>();
            Filter = _messageFilter.AsObservable();

            Filter.Select(filter => filter.NotNull().MessageFilterType)
                .ToPropertyEx(this, model => model.MessageFilterType);

            Filter.Select(filter => filter.NotNull().From ?? DateTime.Now)
                .ToPropertyEx(this, model => model.FromDate);

            Filter.Select(filter => filter.NotNull().To ?? DateTime.Now)
                .ToPropertyEx(this, model => model.ToDate);

            Filter.Select(filter => filter.NotNull().From)
                .Subscribe(fromDate => FromDateText = fromDate?.ToShortDateLocaleString() ?? Strings.FilterFromTitle);

            Filter.Select(filter => filter.NotNull().To)
                .Subscribe(toDate => ToDateText = toDate?.ToShortDateLocaleString() ?? Strings.FilterToTitle);

            Filter.ToPropertyEx(this, model => model.FilterConfiguration);

            SetMessageFilterTypeCommand = ReactiveCommand.Create<MessageFilterType>(DoSetMessageFilterType).NotNull();
            SetFromDateTypeCommand = ReactiveCommand.Create<DateTime>(DoSetFromDate).NotNull();
            SetToDateTypeCommand = ReactiveCommand.Create<DateTime>(DoSetToDate).NotNull();

            _messageFilter.OnNext(_configurationRepository.GetSettings<AllMessageFilterConfiguration>());
        }

        [Reactive] [CanBeNull] public string FromDateText { get; set; }

        [Reactive] [CanBeNull] public string ToDateText { get; set; }

        [NotNull] public IObservable<AllMessageFilterConfiguration> Filter { get; }

        [NotNull] public extern AllMessageFilterConfiguration FilterConfiguration { [ObservableAsProperty] get; }

        public extern MessageFilterType MessageFilterType { [ObservableAsProperty] get; }

        public extern DateTime FromDate { [ObservableAsProperty] get; }

        public extern DateTime ToDate { [ObservableAsProperty] get; }

        [NotNull] public ReactiveCommand<MessageFilterType, Unit> SetMessageFilterTypeCommand { get; }

        [NotNull] public ReactiveCommand<DateTime, Unit> SetFromDateTypeCommand { get; }

        [NotNull] public ReactiveCommand<DateTime, Unit> SetToDateTypeCommand { get; }

        private void DoSetMessageFilterType(MessageFilterType type) { UpdateFilter(filter => filter.NotNull().MessageFilterType = type); }

        private void DoSetFromDate(DateTime fromDate) { UpdateFilter(filter => filter.NotNull().From = fromDate); }

        private void DoSetToDate(DateTime toDate) { UpdateFilter(filter => filter.NotNull().To = toDate); }

        private void UpdateFilter([CanBeNull] Action<AllMessageFilterConfiguration> update)
        {
            update?.Invoke(FilterConfiguration);
            _configurationRepository.SaveSetting(FilterConfiguration);
            _messageFilter.OnNext(FilterConfiguration);
        }
    }
}
