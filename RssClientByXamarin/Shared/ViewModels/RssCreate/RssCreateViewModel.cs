using System;
using System.Reactive;
using Droid.EmbeddedResourse;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;
using Shared.Services;

namespace Shared.ViewModels.RssCreate
{
    public class RssCreateViewModel : ViewModel
    {
        private readonly IRssService _service;
        private readonly INavigator _navigator;

        [Reactive]
        public string Url { get; set; }

        public readonly ReactiveCommand<Unit, Unit> CreateCommand;
        
        public RssCreateViewModel(IRssService service, INavigator navigator)
        {
            _service = service;
            _navigator = navigator;

            Url = Strings.CreateRssUrlDefault;

            CreateCommand = ReactiveCommand.CreateFromTask(() => _service.Create(Url));
            CreateCommand.Subscribe(_ => _navigator.GoBack());
        }
    }
}