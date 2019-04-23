using System;
using Android.OS;
using Android.Views;
using Core.ViewModels.Messages.AllMessagesFilter;
using Droid.Resources;
using Droid.Screens.Navigation;

namespace Droid.Screens.Messages.AllMessagesFilter
{
    public class AllMessagesFilterFragment : BaseFragment<AllMessagesFilterViewModel>
    {
        protected override int LayoutId => Resource.Layout.fragment_all_messages_filter;
        public override bool IsRoot => false;

        protected override void RestoreState(Bundle saved) { }

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
                    ViewModel.ClearFilterCommand.Execute().Subscribe();
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}
