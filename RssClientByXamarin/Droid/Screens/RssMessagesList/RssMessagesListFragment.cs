using System;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Droid.Infrastructure.Collections;
using Droid.NativeExtension;
using Droid.Screens.Base.SwipeButtonRecyclerView;
using Droid.Screens.Navigation;
using DynamicData.Annotations;
using ReactiveUI;
using Shared.Database.Rss;
using Shared.Extensions;
using Shared.ViewModels.RssItemDetail;

namespace Droid.Screens.RssMessagesList
{
    public class RssMessagesListFragment : BaseFragment<RssMessagesListViewModel>
    {
        private RssMessagesFragmentViewHolder _viewHolder;
        
        private string _itemId;

        // ReSharper disable once UnusedMember.Global
        public RssMessagesListFragment() { }

        public RssMessagesListFragment(string itemId) { _itemId = itemId; }

        protected override int LayoutId => Resource.Layout.fragment_rss_detail;
        public override bool IsRoot => false;

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            outState.PutString(nameof(_itemId), _itemId);
        }

        protected override void RestoreState(Bundle saved) { _itemId = saved.GetString(nameof(_itemId)); }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState).NotNull();

            HasOptionsMenu = true;

            _viewHolder = new RssMessagesFragmentViewHolder(view);

            Title = ViewModel.Parameters.RssModel.Name;
            
            var adapter = new RssMessagesListAdapter(Activity, ViewModel.AppConfiguration);
            _viewHolder.RecyclerView.SetAdapter(adapter);

            var callback = new SwipeButtonTouchHelperCallback();
            var touchHelper = new ItemTouchHelper(callback);
            touchHelper.AttachToRecyclerView(_viewHolder.RecyclerView);

            var adapterUpdater = new AdapterUpdater<RssMessageServiceModel>(_viewHolder.RecyclerView, adapter, ViewModel.ListViewModel.SourceList);

            OnActivation((disposable) =>
            {
                ViewModel.ListViewModel.ConnectChanges
                    .Subscribe(w => adapterUpdater.Update(w))
                    .AddTo(disposable);
                
                adapter.GetClickAction()
                    .InvokeCommand(ViewModel.MessageViewModel.OpenContentScreenCommand)
                    .AddTo(disposable);

                adapter.GetLongClickAction()
                    .Subscribe(w => ItemLongClick(w.NotNull().Sender.NotNull(), w.NotNull().EventArgs.NotNull()))
                    .AddTo(disposable);
                
                adapter.GetSwipeLeftAction()
                    .InvokeCommand(ViewModel.MessageViewModel.ChangeReadItemCommand)
                    .AddTo(disposable);
                
                adapter.GetSwipeRightAction()
                    .InvokeCommand(ViewModel.MessageViewModel.ChangeFavoriteCommand)
                    .AddTo(disposable);
                
                _viewHolder.RefreshLayout.GetRefreshAction()
                    .SelectUnit()
                    .InvokeCommand(ViewModel.RefreshCommand)
                    .AddTo(disposable);
                
                ViewModel.RefreshCommand.IsExecuting
                    .Subscribe(w => _viewHolder.RefreshLayout.Refreshing = w)
                    .AddTo(disposable);

                ViewModel.LoadCommand.Execute().NotNull().Subscribe();
            });
            
            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater) { inflater.Inflate(Resource.Menu.menu_rssDetail, menu); }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var rssModel = ViewModel.Parameters.RssModel;
            switch (item.ItemId)
            {
                case Resource.Id.menuItem_rssDetail_remove:
                    ViewModel.RssViewModel.ShowDeleteDialogCommand.Execute(rssModel).Subscribe();
                    break;
                case Resource.Id.menuItem_rssDetail_edit:
                    ViewModel.RssViewModel.OpenEditItemCommand.Execute(rssModel).Subscribe();
                    break;
                case Resource.Id.menuItem_rssDetail_share:
                    ViewModel.RssViewModel.ShareCommand.Execute(rssModel).Subscribe();
                    break;
                case Resource.Id.menuItem_rssDetail_readAllMessages:
                    ViewModel.RssViewModel.ReadAllMessagesCommand.Execute(rssModel).Subscribe();
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }
        
        private void ItemLongClick([NotNull] object sender, [NotNull] RssMessageServiceModel model)
        {
            var menu = new PopupMenu(Activity, sender as View, (int) GravityFlags.Right);
            menu.MenuItemClick += (o, eventArgs) => MenuClick(model.NotNull(), eventArgs.NotNull());
            menu.Inflate(Resource.Menu.contextMenu_rssDetailList);
            menu.Show();
        }

        private void MenuClick([NotNull] RssMessageServiceModel model, [NotNull] PopupMenu.MenuItemClickEventArgs eventArgs)
        {
            switch (eventArgs.Item?.ItemId)
            {
                case Resource.Id.menuItem_rssDetailList_contextShare:
                    ViewModel.MessageViewModel.ShareItemCommand.Execute(model).NotNull().Subscribe();
                    break;
                case Resource.Id.menuItem_rssDetailList_contextRead:
                    ViewModel.MessageViewModel.ChangeReadItemCommand.Execute(model).NotNull().Subscribe();
                    break;
                case Resource.Id.menuItem_rssDetailList_contextFavorite:
                    ViewModel.MessageViewModel.ChangeFavoriteCommand.Execute(model).NotNull().Subscribe();
                    break;
            }
        }
    }
}
