using Core.Infrastructure.Navigation;
using Core.ViewModels.Messages.AllMessages;
using Droid.Screens.Navigation;

namespace Droid.Screens.Messages.AllMessages
{
    public class AllMessagesWay : IWay<AllMessagesViewModel>
    {
        private readonly IFragmentManager _activity;

        public AllMessagesWay(IFragmentManager activity) { _activity = activity; }

        public void Go() { _activity.AddFragment(new AllMessagesFragment()); }
    }
}
