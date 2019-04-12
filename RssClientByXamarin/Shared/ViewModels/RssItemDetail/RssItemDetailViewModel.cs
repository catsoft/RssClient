using Shared.Infrastructure.ViewModels;

namespace Shared.ViewModels.RssItemDetail
{
    public class RssItemDetailViewModel : ViewModelWithParameter<RssItemDetailParameterses>
    {
        public RssItemDetailViewModel(RssItemDetailParameterses parameters) : base(parameters)
        {
        }
    }
}
