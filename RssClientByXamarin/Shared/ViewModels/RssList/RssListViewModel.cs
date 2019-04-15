using System.Collections.Generic;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Droid.Repository.Configuration;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shared.Configuration.Settings;
using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;
using Shared.Services.Rss;
using Shared.ViewModels.RssAllMessages;
using Shared.ViewModels.RssCreate;
using Shared.ViewModels.RssListEdit;

namespace Shared.ViewModels.RssList
{
    public class RssListViewModel : ViewModel
    {
        private readonly INavigator _navigator;
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IRssService _rssService;
        
        public RssListViewModel(INavigator navigator)
        {
            _navigator = navigator;

            OpenCreateScreenCommand = ReactiveCommand.Create(OpenCreateScreen);
            OpenAllMessagesScreenCommand = ReactiveCommand.Create(OpenAllMessagesScreen);
            OpenListEditScreenCommand = ReactiveCommand.Create(OpenListEditScreen);
            
            GetListCommand = ReactiveCommand.CreateFromTask(token => _rssService.GetListAsync(token));
            GetListCommand.ToPropertyEx(this, model => model.RssServiceModels);
            
            AppConfiguration = _configurationRepository.GetSettings<AppConfiguration>();
            
        }

        public ReactiveCommand<Unit, Unit> OpenCreateScreenCommand { get; }
        
        public ReactiveCommand<Unit, Unit> OpenAllMessagesScreenCommand { get; }
        
        public ReactiveCommand<Unit, Unit> OpenListEditScreenCommand { get; }
        
        public ReactiveCommand<Unit, IEnumerable<RssServiceModel>> GetListCommand { get; }
        
        public extern IEnumerable<RssServiceModel> RssServiceModels { [ObservableAsProperty] get; }
        
        public AppConfiguration AppConfiguration { get; }

        private void OpenCreateScreen()
        {
            var way = App.Container.Resolve<IWay<RssCreateViewModel>>();
            _navigator.Go(way);
        }
        
        private void OpenAllMessagesScreen()
        {
            var way = App.Container.Resolve<IWay<RssAllMessagesViewModel>>();
            _navigator.Go(way);
        }
        
        private void OpenListEditScreen()
        {
            var way = App.Container.Resolve<IWay<RssListEditViewModel>>();
            _navigator.Go(way);
        }
    }
}