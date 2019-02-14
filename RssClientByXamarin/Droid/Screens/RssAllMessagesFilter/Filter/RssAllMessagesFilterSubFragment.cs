using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.Browser.BrowserActions;
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

            var filterConfiguration = _configurationRepository.GetSettings<AllMessageFilterConfiguration>();

            var favoriteCheckBox = view.FindViewById<CheckBox>(Resource.Id.checkBox_AllMessagesFilter_favorite);
            var readCheckBox = view.FindViewById<CheckBox>(Resource.Id.checkBox_AllMessagesFilter_read);
            var unreadCheckBox = view.FindViewById<CheckBox>(Resource.Id.checkBox_AllMessagesFilter_unread);
            // TODO добавить исчо дату
            var fromButton = view.FindViewById<Button>(Resource.Id.button_AllMessagesFilter_dateFrom);
            var toButton= view.FindViewById<Button>(Resource.Id.button_AllMessagesFilter_dateTo);

            favoriteCheckBox.Checked = filterConfiguration.IsFavorite;
            readCheckBox.Checked = filterConfiguration.IsRead;
            unreadCheckBox.Checked = filterConfiguration.IsUnread;

            favoriteCheckBox.CheckedChange += (sender, args) =>
            {
                var filter = _configurationRepository.GetSettings<AllMessageFilterConfiguration>();
                filter.IsFavorite = args.IsChecked;
                _configurationRepository.SaveSetting(filter);
            };
            
            readCheckBox.CheckedChange += (sender, args) =>
            {
                var filter = _configurationRepository.GetSettings<AllMessageFilterConfiguration>();
                filter.IsRead = args.IsChecked;
                _configurationRepository.SaveSetting(filter);
            };
            
            unreadCheckBox.CheckedChange += (sender, args) =>
            {
                var filter = _configurationRepository.GetSettings<AllMessageFilterConfiguration>();
                filter.IsUnread = args.IsChecked;
                _configurationRepository.SaveSetting(filter);
            };
            
            return view;
        }
    }
}