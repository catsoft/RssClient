using Core.Infrastructure.Navigation;
using Core.ViewModels.Messages.AllMessagesFilter;
using Droid.Screens.Navigation;

namespace Droid.Screens.Messages.AllMessagesFilter
{
    public class AllMessagesFilterWay : IWay<AllMessagesFilterViewModel>
    {
        private readonly IFragmentManager _activity;

        public AllMessagesFilterWay(IFragmentManager activity) { _activity = activity; }

        public void Go() { _activity.AddFragment(new AllMessagesFilterFragment()); }
    }
}
