using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Autofac;
using Droid.Container;
using Droid.Repository.Configuration;
using Droid.Screens.Base.SwipeRecyclerView;
using Droid.Screens.Navigation;
using ReactiveUI;
using Shared;
using Shared.Configuration.Settings;
using Shared.Extensions;
using Shared.Infrastructure.Navigation;
using Shared.Repository.Rss;
using Shared.Services.Rss;
using Shared.ViewModels.RssAllMessages;
using Shared.ViewModels.RssCreate;
using Shared.ViewModels.RssList;
using Shared.ViewModels.RssListEdit;

namespace Droid.Screens.RssList
{
    public class RssListFragment : BaseFragment<RssListViewModel>
    {
        private RssListFragmentViewHolder _viewHolder;
        
        protected override int LayoutId => Resource.Layout.fragment_rss_list;
        public override bool IsRoot => true;

        public RssListFragment()
        {

        }

        protected override void RestoreState(Bundle saved)
        {

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            
            Title = Activity?.GetText(Resource.String.rssList_title);

            HasOptionsMenu = true;

            _viewHolder = new RssListFragmentViewHolder(view);

            OnActivation(disposable =>
            {
                this.BindCommand(ViewModel, model => model.OpenCreateScreenCommand,
                    fragment => fragment._viewHolder.FloatingActionButton.Events().Click)
                    .AddTo(disposable);

                ViewModel.WhenAnyValue(w => w.RssServiceModels)
                    .Subscribe(UpdateAdapter)
                    .AddTo(disposable);
            });
            
            return view;
        }

        private void UpdateAdapter(IEnumerable<RssServiceModel> rssServiceModels)
        {
            var adapter = new RssListAdapter(rssServiceModels.ToList(), Activity, ViewModel.AppConfiguration);
            _viewHolder.RecyclerView.SetAdapter(adapter);
            adapter.NotifyDataSetChanged();

            var callback = new SwipeTouchHelperCallback(adapter);
            var touchHelper = new ItemTouchHelper(callback);
            touchHelper.AttachToRecyclerView(_viewHolder.RecyclerView);
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.menu_rssList, menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuItem_rssList_change:
                    ViewModel.OpenAllMessagesScreenCommand.ExecuteNow();
                    break;
                
                case Resource.Id.menuItem_rssList_editMode:
                    ViewModel.OpenListEditScreenCommand.ExecuteNow();
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}