using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Core.Configuration.AllMessageFilter;
using Core.Extensions;
using Core.ViewModels.Messages.AllMessagesFilter;
using Droid.Resources;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;

namespace Droid.Screens.Messages.AllMessagesFilter.Filter
{
    public class AllMessagesFilterSubFragment : BaseFragment<AllMessagesFilterFilterViewModel>, RadioGroup.IOnCheckedChangeListener
    {
        [NotNull] private AllMessagesFilterSubFragmentViewHolder _viewHolder;

        // ReSharper disable once EmptyConstructor
        // ReSharper disable once NotNullMemberIsNotInitialized
        public AllMessagesFilterSubFragment() { }

        protected override int LayoutId => Resource.Layout.fragment_all_messages_filter_sub;

        public override bool IsRoot => false;

        public void OnCheckedChanged(RadioGroup group, int checkedId)
        {
            MessageFilterType filterType;
            switch (checkedId)
            {
                default:
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

            ViewModel.SetMessageFilterTypeCommand.Execute(filterType).NotNull().Subscribe();
        }

        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            _viewHolder = new AllMessagesFilterSubFragmentViewHolder(view.NotNull());

            _viewHolder.RootRadioGroup.SetOnCheckedChangeListener(this);

            OnActivation(disposable =>
            {
                ViewModel.WhenAnyValue(model => model.MessageFilterType)
                    .NotNull()
                    .Subscribe(SetFilterType)
                    .AddTo(disposable);

                this.Bind(ViewModel, model => model.FromDateText, fragment => fragment._viewHolder.FromButton.Text)
                    .AddTo(disposable);

                this.Bind(ViewModel, model => model.ToDateText, fragment => fragment._viewHolder.ToButton.Text)
                    .AddTo(disposable);

                _viewHolder.FromButton.Events()
                    ?.Click?
                    .Subscribe(w => OpenFromDatePicker())
                    .AddTo(disposable);

                _viewHolder.ToButton.Events()
                    ?.Click?
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
            var picker = new DatePickerDialog(Context, SetFromDate, fromDate.Year, fromDate.Month - 1, fromDate.Day);
            picker.Show();
        }

        private void OpenToDatePicker()
        {
            var defaultDate = ViewModel.ToDate;
            var picker = new DatePickerDialog(Context, SetToDate, defaultDate.Year, defaultDate.Month - 1, defaultDate.Day);
            picker.Show();
        }

        private void SetFromDate(object sender, [NotNull] DatePickerDialog.DateSetEventArgs e)
        {
            ViewModel.SetFromDateTypeCommand.Execute(e.Date).NotNull().Subscribe();
        }

        private void SetToDate(object sender, [NotNull] DatePickerDialog.DateSetEventArgs e)
        {
            ViewModel.SetToDateTypeCommand.Execute(e.Date).NotNull().Subscribe();
        }
    }
}
