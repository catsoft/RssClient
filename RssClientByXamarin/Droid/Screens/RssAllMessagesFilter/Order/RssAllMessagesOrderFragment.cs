using Android.OS;
using Android.Views;
using Android.Widget;
using Droid.Container;
using Droid.Repository;
using Droid.Screens.Navigation;
using Shared.Configuration.Settings;

namespace Droid.Screens.RssAllMessagesFilter.Order
{
    public class RssAllMessagesOrderFragment : SubFragment, RadioGroup.IOnCheckedChangeListener
    {
        [Inject]
        private IConfigurationRepository _configurationRepository;
        
        protected override int LayoutId => Resource.Layout.fragment_all_messages_order_sub;

        public RssAllMessagesOrderFragment()
        {
            
        }

        protected override void RestoreState(Bundle saved)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var filterConfiguration = _configurationRepository.GetSettings<AllMessageFilterConfiguration>();

            var rootRadioGroup =  View.FindViewById<RadioGroup>(Resource.Id.radioGroup_rss_all_messages_order_main);
            var newestRadioButton = View.FindViewById<RadioButton>(Resource.Id.radioButton_rss_all_messages_order_newest);
            var oldestRadioButton = View.FindViewById<RadioButton>(Resource.Id.radioButton_rss_all_messages_order_oldest);
            
            switch (filterConfiguration.Sort)
            {
                case Sort.Oldest:
                    oldestRadioButton.Checked = true;
                    break;
                case Sort.Newest:
                    newestRadioButton.Checked = true;
                    break;
            }

            rootRadioGroup.SetOnCheckedChangeListener(this);
            
            return view;
        }

        public void OnCheckedChanged(RadioGroup @group, int checkedId)
        {
            var filterConfiguration = _configurationRepository.GetSettings<AllMessageFilterConfiguration>();

            switch (checkedId)
            {
                case Resource.Id.radioButton_rss_all_messages_order_newest:
                    filterConfiguration.Sort = Sort.Newest;
                    break;
                case Resource.Id.radioButton_rss_all_messages_order_oldest:
                    filterConfiguration.Sort = Sort.Oldest;
                    break;
            }
            
            _configurationRepository.SaveSetting(filterConfiguration);
        }
    }
}