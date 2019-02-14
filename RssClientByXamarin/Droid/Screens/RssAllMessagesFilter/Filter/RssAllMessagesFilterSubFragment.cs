using Android.OS;
using Android.Views;
using Droid.Container;
using Droid.Repository;
using Droid.Screens.Navigation;
using Shared.Configuration.Settings;

namespace Droid.Screens.RssAllMessagesFilter.Filter
{
    public class RssAllMessagesFilterSubFragment : SubFragment
    {
        [Inject]
        private IConfigurationRepository _configurationRepository;
        
        protected override int LayoutId => Resource.Layout.fragment_all_messages_filter_sub;

        public RssAllMessagesFilterSubFragment()
        {
            
        }

        protected override void RestoreState(Bundle saved)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var filter = _configurationRepository.GetSettings<AllMessageFilterConfiguration>();
            
            return view;
        }
    }
}