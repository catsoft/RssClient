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

            LoadCommand = ReactiveCommand.CreateFromTask<Unit, RssData>(s => _rssService.Find(parameters.RssId));

            LoadCommand.Subscribe(data =>
            {
                RssData = data; 
                Url = data.Rss;
            });
            
            UpdateCommand = ReactiveCommand.CreateFromTask(_ => _rssService.Update(RssData.Id, Url));
            UpdateCommand.Subscribe(_ => _navigator.GoBack());
        }

        [Reactive]
        public string Url { get; set; }

        public RssData RssData { get; set; }
        
        public ReactiveCommand<Unit, RssData> LoadCommand { get; }
        
        public ReactiveCommand<Unit, Unit> UpdateCommand { get; }
    }
}