using Shared.Infrastructure.ViewModels;

namespace Shared.ViewModels.RssMessage
{
    public class RssMessageViewModel : ViewModelWithParameter<RssMessageParameters>
    {
        public RssMessageViewModel(RssMessageParameters parameters) : base(parameters) { }
    }
}
