using System;
using System.Globalization;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.Browser.BrowserActions;
using Droid.Container;
using Droid.Repository;
using Droid.Screens.Navigation;
using Shared.Configuration.Settings;
using Shared.Services;
using Shared.Services.Locale;

namespace Droid.Screens.RssAllMessagesFilter.Filter
{
    public class RssAllMessagesFilterSubFragment : SubFragment, RadioGroup.IOnCheckedChangeListener
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

            var rootRadioGroup = view.FindViewById<RadioGroup>(Resource.Id.radioGroup_rss_all_messages_filter_main);
            var allRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_rss_all_messages_filter_all);
            var favoriteRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_rss_all_messages_filter_favorite);
            var readRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_rss_all_messages_filter_read);
            var unreadRadioButton = view.FindViewById<RadioButton>(Resource.Id.radioButton_rss_all_messages_filter_unread);

            switch (filterConfiguration.MessageFilterType)
            {
                case MessageFilterType.None:
                    allRadioButton.Checked = true;
                    break;
                case MessageFilterType.Favorite:
                    favoriteRadioButton.Checked = true;
                    break;
                case MessageFilterType.Read:
                    readRadioButton.Checked = true;
                    break;
                case MessageFilterType.Unread:
                    unreadRadioButton.Checked = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            rootRadioGroup.SetOnCheckedChangeListener(this);

            var fromButton = view.FindViewById<Button>(Resource.Id.button_AllMessagesFilter_dateFrom);
            var toButton = view.FindViewById<Button>(Resource.Id.button_AllMessagesFilter_dateTo);

            if (filterConfiguration.From.HasValue)
            {
                fromButton.Text = filterConfiguration.From.Value.ToShortDateLocaleString();
            }
            
            if (filterConfiguration.To.HasValue)
            {
                toButton.Text = filterConfiguration.To.Value.ToShortDateLocaleString();
            }

            fromButton.Click += (sender, args) =>
            {
                var configuration = _configurationRepository.GetSettings<AllMessageFilterConfiguration>();
                var defaultDate = configuration.From ?? DateTime.Now;
                var picker = new DatePickerDialog(Context, SetFromDate, defaultDate.Year,defaultDate.Month,defaultDate.Day);
                picker.Show();
            };

            toButton.Click += (sender, args) =>
            {
                var configuration = _configurationRepository.GetSettings<AllMessageFilterConfiguration>();
                var defaultDate = configuration.To ?? DateTime.Now;
                var picker = new DatePickerDialog(Context, SetToDate, defaultDate.Year,defaultDate.Month,defaultDate.Day);
                picker.Show();
            };

            return view;
        }

        private void SetFromDate(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            var filterConfiguration = _configurationRepository.GetSettings<AllMessageFilterConfiguration>();
            filterConfiguration.From = e.Date;
            _configurationRepository.SaveSetting(filterConfiguration);
            
            var fromButton = View.FindViewById<Button>(Resource.Id.button_AllMessagesFilter_dateFrom);
            fromButton.Text = e.Date.ToShortDateLocaleString();
        }
        
        private void SetToDate(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            var filterConfiguration = _configurationRepository.GetSettings<AllMessageFilterConfiguration>();
            filterConfiguration.To = e.Date;
            _configurationRepository.SaveSetting(filterConfiguration);
            
            var toButton = View.FindViewById<Button>(Resource.Id.button_AllMessagesFilter_dateTo);
            toButton.Text = e.Date.ToShortDateLocaleString();
        }
        
        public void OnCheckedChanged(RadioGroup @group, int checkedId)
        {
            var filterConfiguration = _configurationRepository.GetSettings<AllMessageFilterConfiguration>();

            switch (checkedId)
            {
                case Resource.Id.radioButton_rss_all_messages_filter_all:
                    filterConfiguration.MessageFilterType = MessageFilterType.None;
                    break;
                case Resource.Id.radioButton_rss_all_messages_filter_favorite:
                    filterConfiguration.MessageFilterType = MessageFilterType.Favorite;
                    break;
                case Resource.Id.radioButton_rss_all_messages_filter_read:
                    filterConfiguration.MessageFilterType = MessageFilterType.Read;
                    break;
                case Resource.Id.radioButton_rss_all_messages_filter_unread:
                    filterConfiguration.MessageFilterType = MessageFilterType.Unread;
                    break;
            }
            
            _configurationRepository.SaveSetting(filterConfiguration);
        }
    }
}