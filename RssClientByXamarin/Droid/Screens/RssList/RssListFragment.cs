using System;
using System.Reactive;
using System.Reactive.Linq;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Droid.Infrastructure.Collections;
using Droid.NativeExtension;
using Droid.Screens.Base.SwipeRecyclerView;
using Droid.Screens.Navigation;
using ReactiveUI;
using Shared.Extensions;
using Shared.Services.Rss;
using Shared.ViewModels.RssList;

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

            var adapter = new RssListAdapter(Activity, ViewModel.AppConfiguration);
            _viewHolder.RecyclerView.SetAdapter(adapter);
            adapter.NotifyDataSetChanged();

            var callback = new SwipeTouchHelperCallback(adapter);
            var touchHelper = new ItemTouchHelper(callback);
            touchHelper.AttachToRecyclerView(_viewHolder.RecyclerView);

            var adapterUpdater = new AdapterUpdater<RssServiceModel>(adapter, ViewModel.SourceList);
            
            OnActivation(disposable =>
            {
                _viewHolder.FloatingActionButton.Events().Click
                    .Select(w => Unit.Default)
                    .InvokeCommand(ViewModel, (model => model.OpenCreateScreenCommand))
                    .AddTo(disposable);
                
                ViewModel.WhenAnyValue(w => w.IsEmpty)
                    .Subscribe(w => _viewHolder.EmptyTextView.Visibility = w.ToVisibility())
                    .AddTo(disposable);
                
                adapter.GetRssItemClickEvent()
                    .InvokeCommand(ViewModel.OpenDetailScreenCommand)
                    .AddTo(disposable);
                
                adapter.GetRssItemLongClickEvent()
                    .Subscribe(model => ItemLongClick(model.EventArgs, model.Sender))
                    .AddTo(disposable);
                
                adapter.GetRssItemDismissEvent()
                    .InvokeCommand(ViewModel.ItemRemoveCommand)
                    .AddTo(disposable);

                ViewModel.ConnectChanges()
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(w => adapterUpdater.Update(w))
                    .AddTo(disposable);

                ViewModel.GetListCommand.Execute().Subscribe();
            });
            
            return view;
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
        
        private void ItemLongClick(RssServiceModel holderItem, object sender)
        {
            var menu = new PopupMenu(Activity, sender as View, (int) GravityFlags.Right);
            menu.MenuItemClick += (o, eventArgs) => MenuClick(holderItem, eventArgs);
            menu.Inflate(Resource.Menu.contextMenu_rssList);
            menu.Show();
        }

        private void MenuClick(RssServiceModel holderItem, PopupMenu.MenuItemClickEventArgs eventArgs)
        {
            switch (eventArgs.Item.ItemId)
            {
                case Resource.Id.menuItem_rssList_contextEdit:
                    ViewModel.OpenEditItemScreenCommand.ExecuteNow(holderItem);
                    break;
                case Resource.Id.menuItem_rssList_contextRemove:
                    ViewModel.ShowDeleteItemDialogCommand.ExecuteNow(holderItem);
                    break;
                case Resource.Id.menuItem_rssList_contextShare:
                    ViewModel.ShareItemCommand.ExecuteNow(holderItem);
                    break;
                case Resource.Id.menuItem_rssList_contextReadAllMessages:
                    ViewModel.ReadAllItemsCommand.ExecuteNow(holderItem);
                    break;
            }
        }
    }
}