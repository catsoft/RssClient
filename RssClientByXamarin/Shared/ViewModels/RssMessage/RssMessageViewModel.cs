using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;
using Shared.ViewModels.RssAllMessages;

namespace Shared.ViewModels.RssMessage
{
    public class RssMessageViewModel : ViewModelWithParameter<RssMessageParameterses>
    {
        public RssMessageViewModel(RssMessageParameterses parameterse) : base(parameterse)
        {
        }
    }
}