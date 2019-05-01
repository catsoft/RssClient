using System;
using System.Reactive.Linq;
using Android.OS;
using Android.Views;
using Android.Widget;
using Core.Configuration.AllMessageFilter;
using Core.Extensions;
using Core.ViewModels.Messages.AllMessagesFilter;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;

namespace Droid.Screens.Messages.AllMessagesFilter.Order
{
    public class AllMessagesOrderFragment : BaseFragment<AllMessagesOrderFilterViewModel>
    {
        // ReSharper disable once NotNullMemberIsNotInitialized
        [NotNull] private AllMessagesOrderFragmentViewHolder _viewHolder;

        protected override int LayoutId => Resource.Layout.fragment_all_messages_order_sub;

        public override bool IsRoot => false;

        // ReSharper disable once NotNullMemberIsNotInitialized
        // ReSharper disable once EmptyConstructor
        public AllMessagesOrderFragment()
        {
        }

        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState).NotNull();

            _viewHolder = new AllMessagesOrderFragmentViewHolder(view);

            OnActivation(disposable =>
            {
                ViewModel.WhenAnyValue(w => w.Sort)
                    .NotNull()
                    .Subscribe(UpdateSortFilter)
                    .AddTo(disposable);
                
                _viewHolder.RootRadioGroup.Events()
                    .NotNull()
                    .CheckedChange
                    .NotNull()
                    .Select(w => w.NotNull().CheckedId)
                    .Select(ConvertToSort)
                    .InvokeCommand(ViewModel.UpdateSortCommand)
                    .AddTo(disposable);
            });

            return view;
        }

        private Sort ConvertToSort(int id)
        {
            return id == _viewHolder.OldestRadioButton.Id ? Sort.Oldest : Sort.Newest;
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
