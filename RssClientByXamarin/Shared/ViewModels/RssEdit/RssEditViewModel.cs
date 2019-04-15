using System;
using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;
using Shared.Repository.Rss;
using Shared.Services;

namespace Shared.ViewModels.RssEdit
{
    public class RssEditViewModel : ViewModelWithParameter<RssEditParameters>
    {
        private readonly IRssService _rssService;
        private readonly INavigator _navigator;

        public RssEditViewModel(IRssService rssService, RssEditParameters parameters, INavigator navigator) : base(parameters)
        {
            _rssService = rssService;
            _navigator = navigator;

            LoadCommand = ReactiveCommand.CreateFromTask<Unit, RssData>((s, token) => _rssService.Find(parameters.RssId, token));
            LoadCommand.ToPropertyEx(this, x => x.RssData);
            
            UpdateCommand = ReactiveCommand.CreateFromTask(token => _rssService.Update(RssData.Id, Url, token));
            UpdateCommand.Subscribe(_ => _navigator.GoBack());

            this.WhenAnyValue(w => w.RssData).Subscribe(w => Url = w?.Rss);
        }

        [Reactive]
        public string Url { get; set; }

        public extern RssData RssData { [ObservableAsProperty] get; }

        public ReactiveCommand<Unit, RssData> LoadCommand { get; }
        
        public ReactiveCommand<Unit, Unit> UpdateCommand { get; }
    }
}