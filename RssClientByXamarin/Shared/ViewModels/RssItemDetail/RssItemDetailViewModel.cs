using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;

namespace Shared.ViewModels.RssItemDetail
{
    public class RssItemDetailViewModel : ViewModelWithParameter<RssItemDetailParameterses>
    {
        public RssItemDetailViewModel(RssItemDetailParameterses parameterse) : base(parameterse)
        {
        }
    }
}
