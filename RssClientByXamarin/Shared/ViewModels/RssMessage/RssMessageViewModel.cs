#region

using Shared.Infrastructure.ViewModels;

#endregion

namespace Shared.ViewModels.RssMessage
{
    public class RssMessageViewModel : ViewModelWithParameter<RssMessageParameterses>
    {
        public RssMessageViewModel(RssMessageParameterses parameters) : base(parameters) { }
    }
}
