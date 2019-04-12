using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;

namespace Shared.ViewModels.RssEdit
{
    public class RssEditViewModel : ViewModelWithParameter<RssEditParameterses>
    {
        public RssEditViewModel(RssEditParameterses parameterse) : base(parameterse)
        {
        }
    }
}