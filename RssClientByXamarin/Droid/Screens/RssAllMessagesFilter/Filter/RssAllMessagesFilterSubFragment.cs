using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Droid.Screens.Navigation;
using ReactiveUI;
using Shared.Configuration.Settings;
using Shared.Extensions;
using Shared.ViewModels.RssAllMessagesFilter;

namespace Droid.Screens.RssAllMessagesFilter.Filter
{
    public class RssAllMessagesFilterSubFragment : BaseFragment<RssAllMessagesFilterFilterViewModel>, RadioGroup.IOnCheckedChangeListener
    {
        private RssAllMessagesFilterSubFragmentViewHolder _viewHolder;
        
        protected override int LayoutId => Resource.Layout.fragment_all_messages_filter_sub;

        public override bool IsRoot => false;
        
        public RssAllMessagesFilterSubFragment()
        {
            
        }

        protected override void RestoreState(Bundle saved)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            _viewHolder = new RssAllMessagesFilterSubFragmentViewHolder(view);

            _viewHolder.RootRadioGroup.SetOnCheckedChangeListener(this);
            
            OnActivation(disposable =>
            {
                ViewModel.WhenAnyValue(model => model.MessageFilterType)
                    .Subscribe(SetFilterType)
                    .AddTo(disposable);
                
                this.Bind(ViewModel, model => model.FromDateText, fragment => fragment._viewHolder.FromButton.Text)
                    .AddTo(disposable);

                this.Bind(ViewModel, model => model.ToDateText, fragment => fragment._viewHolder.ToButton.Text)
                    .AddTo(disposable);

                _viewHolder.FromButton.Events().Click
                    .Subscribe(w => OpenFromDatePicker())
                    .AddTo(disposable);
                
                _viewHolder.ToButton.Events().Click
                    .Subscribe(w => OpenToDatePicker())
                    .AddTo(disposable);
            });

            return view;
        }

        private void SetFilterType(MessageFilterType type)
        {
            switch (type)
            {
                default:
                case MessageFilterType.None:
                    _viewHolder.AllRadioButton.Checked = true;
                    break;
                case MessageFilterType.Favorite:
                    _viewHolder.FavoriteRadioButton.Checked = true;
                    break;
                case MessageFilterType.Read:
                    _viewHolder.ReadRadioButton.Checked = true;
                    break;
                case MessageFilterType.Unread:
                    _viewHolder.UnreadRadioButton.Checked = true;
                    break;
            }
        }

        private void OpenFromDatePicker()
        {
            var fromDate = ViewModel.FromDate;
            var picker = new DatePickerDialog(Context, SetFromDate, fromDate.Year,fromDate.Month,fromDate.Day);
            picker.Show();   
        }

        private void OpenToDatePicker()
        {
            var defaultDate = ViewModel.ToDate;
            var picker = new DatePickerDialog(Context, SetToDate, defaultDate.Year,defaultDate.Month,defaultDate.Day);
            picker.Show();
        }
        
        private void SetFromDate(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            ViewModel.SetFromDateTypeCommand.Execute(e.Date).Subscribe();
        }
        
        private void SetToDate(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            ViewModel.SetToDateTypeCommand.Execute(e.Date).Subscribe();
        }
        
        public void OnCheckedChanged(RadioGroup @group, int checkedId)
        {
            MessageFilterType filterType;
            switch (checkedId)
            {
                default:
                case Resource.Id.radioButton_rss_all_messages_filter_all:
                    filterType = MessageFilterType.None;
                    break;
                case Resource.Id.radioButton_rss_all_messages_filter_favorite:
                    filterType = MessageFilterType.Favorite;
                    break;
                case Resource.Id.radioButton_rss_all_messages_filter_read:
                    filterType = MessageFilterType.Read;
                    break;
                case Resource.Id.radioButton_rss_all_messages_filter_unread:
                    filterType = MessageFilterType.Unread;
                    break;
            }

            ViewModel.SetMessageFilterTypeCommand.Execute(filterType).Subscribe();
        }
    }
}