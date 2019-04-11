using Android.OS;
using Android.Views;
using Droid.Container;
using Droid.Repository.Configuration;
using Droid.Screens.Navigation;
using Shared.Configuration.Settings;
using Shared.Infrastructure.Navigation;

namespace Droid.Screens.RssAllMessagesFilter
{
    public class RssAllMessagesFilterFragment : TitleFragment
    {
        [Inject]
        private IConfigurationRepository _configurationRepository;

        [Inject]
        private INavigator _navigator;
    
        protected override int LayoutId => Resource.Layout.fragment_all_messages_filter;
        public override bool RootFragment => false;
        
        protected override void RestoreState(Bundle saved)
        {
        }

        public RssAllMessagesFilterFragment()
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            Title = GetText(Resource.String.all_messages_filter_title);

            HasOptionsMenu = true;
            
            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            base.OnCreateOptionsMenu(menu, inflater);
            
            inflater.Inflate(Resource.Menu.menu_allMessagesFilter, menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuItem_allMessagesFilter_clear:
                    ClearFilter();
                    return true;
            }
            
            return base.OnOptionsItemSelected(item);
        }

        private void ClearFilter()
        {
            _configurationRepository.DeleteSetting<AllMessageFilterConfiguration>();
         
            _navigator.GoBack();
        }
    }
}