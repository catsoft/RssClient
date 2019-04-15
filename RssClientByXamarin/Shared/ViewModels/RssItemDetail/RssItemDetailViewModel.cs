using Shared.Infrastructure.ViewModels;

namespace Shared.ViewModels.RssItemDetail
{
    public class RssItemDetailViewModel : ViewModelWithParameter<RssItemDetailParameters>
    {
        public RssItemDetailViewModel(RssItemDetailParameters parameters) : base(parameters)
        {
        }
    }
}
