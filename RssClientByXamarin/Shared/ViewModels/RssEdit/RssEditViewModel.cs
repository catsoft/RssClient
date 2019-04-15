using System;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;
using Shared.Repository.Rss;
using Shared.Services;
using Shared.Services.Rss;

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

            LoadCommand = ReactiveCommand.CreateFromTask<Unit, RssServiceModel>((s, token) => _rssService.GetAsync(parameters.RssId, token));
            LoadCommand.ToPropertyEx(this, x => x.RssServiceModel);
            
            UpdateCommand = ReactiveCommand.CreateFromTask(UpdateRssUrl);
            UpdateCommand.Subscribe(_ => _navigator.GoBack());

            this.WhenAnyValue(w => w.RssServiceModel).Subscribe(w => Url = w?.Rss);
        }

        private Task UpdateRssUrl(CancellationToken token)
        {
            var model = RssServiceModel;
            model.Rss = Url;
            return _rssService.UpdateAsync(model, token);
        }

        [Reactive]
        public string Url { get; set; }

        public extern RssServiceModel RssServiceModel { [ObservableAsProperty] get; }

        public ReactiveCommand<Unit, RssServiceModel> LoadCommand { get; }
        
        public ReactiveCommand<Unit, Unit> UpdateCommand { get; }
    }
}