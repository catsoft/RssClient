#region

using System;
using Android.OS;
using Android.Views;
using Android.Widget;
using Droid.Screens.Navigation;
using ReactiveUI;
using Shared.Configuration.Settings;
using Shared.Extensions;
using Shared.ViewModels.RssAllMessagesFilter;

#endregion

namespace Droid.Screens.RssAllMessagesFilter.Order
{
    public class RssAllMessagesOrderFragment : BaseFragment<RssAllMessagesOrderFilterViewModel>, RadioGroup.IOnCheckedChangeListener
    {
        private RssAllMessagesOrderFragmentViewHolder _viewHolder;

        protected override int LayoutId => Resource.Layout.fragment_all_messages_order_sub;

        public override bool IsRoot => false;

        public void OnCheckedChanged(RadioGroup group, int checkedId)
        {
            Sort sort;

            switch (checkedId)
            {
                default:
                case Resource.Id.radioButton_rss_all_messages_order_newest:
                    sort = Sort.Newest;
                    break;
                case Resource.Id.radioButton_rss_all_messages_order_oldest:
                    sort = Sort.Oldest;
                    break;
            }

            ViewModel.UpdateSortCommand.Execute(sort).Subscribe();
        }

        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            _viewHolder = new RssAllMessagesOrderFragmentViewHolder(view);

            _viewHolder.RootRadioGroup.SetOnCheckedChangeListener(this);

            OnActivation(disposable =>
            {
                ViewModel.WhenAnyValue(w => w.Sort)
                    .Subscribe(UpdateSortFilter)
                    .AddTo(disposable);
            });

            return view;
        }

        private void UpdateSortFilter(Sort sort)
        {
            switch (sort)
            {
                case Sort.Oldest:
                    _viewHolder.OldestRadioButton.Checked = true;
                    break;
                case Sort.Newest:
                    _viewHolder.NewestRadioButton.Checked = true;
                    break;
            }
        }
    }
}
