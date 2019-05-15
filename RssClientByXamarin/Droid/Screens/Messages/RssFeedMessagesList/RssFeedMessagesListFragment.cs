using System;
using System.Reactive.Linq;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Core.Extensions;
using Core.Services.RssMessages;
using Core.ViewModels.Messages.RssFeedMessagesList;
using Droid.Infrastructure.Collections;
using Droid.NativeExtension;
using Droid.NativeExtension.Events;
using Droid.Screens.Base.SwipeButtonRecyclerView;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;

namespace Droid.Screens.Messages.RssFeedMessagesList
{
    public class RssFeedMessagesListFragment : BaseFragment<RssFeedMessagesListViewModel>
    {
        [NotNull] private RssFeedMessagesFragmentViewHolder _viewHolder;
        
        private Guid _itemId;

        // ReSharper disable once UnusedMember.Global
        // ReSharper disable once NotNullMemberIsNotInitialized
        public RssFeedMessagesListFragment() { }

        // ReSharper disable once NotNullMemberIsNotInitialized
        public RssFeedMessagesListFragment(Guid itemId) { _itemId = itemId; }

        protected override int LayoutId => Resource.Layout.fragment_rss_detail;
        public override bool IsRoot => false;

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            outState.PutString(nameof(_itemId), _itemId.ToString());
        }

        protected override void RestoreState(Bundle saved)
        {
            _itemId = Guid.Parse(saved.GetString(nameof(_itemId)));
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState).NotNull();

            HasOptionsMenu = true;

            _viewHolder = new RssFeedMessagesFragmentViewHolder(view);

            Title = ViewModel.Parameters.RssFeedModel.Name;
            
            var adapter = new RssFeedMessagesListAdapter(Activity, ViewModel.AppConfigurationViewModel.AppConfiguration);
            _viewHolder.RecyclerView.SetAdapter(adapter);

            var callback = new SwipeButtonTouchHelperCallback();
            var touchHelper = new ItemTouchHelper(callback);
            touchHelper.AttachToRecyclerView(_viewHolder.RecyclerView);

            var adapterUpdater = new AdapterUpdater<RssMessageServiceModel>(_viewHolder.RecyclerView, adapter, ViewModel.ListViewModel.SourceList);

            OnActivation((disposable) =>
            {
                ViewModel.ListViewModel.ConnectChanges
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(w => adapterUpdater.Update(w))
                    .AddTo(disposable);
                
                this.BindCommand(ViewModel, model => model.ReadAllMessagesCommand, fragment => fragment._viewHolder.ReadAllFloatingActionButton)
                    .AddTo(disposable);
                
                ViewModel.ReadAllMessagesCommand.CanExecute
                    .Select(w => w.ToVisibility())
                    .BindTo(_viewHolder.ReadAllFloatingActionButton, button => button.Visibility)
                    .AddTo(disposable);
                
                adapter.GetClickAction()
                    .InvokeCommand(ViewModel.MessageItemViewModel.HandleItemClickCommand)
                    .AddTo(disposable);

                adapter.GetLongClickAction()
                    .Subscribe(w => ItemLongClick(w.NotNull().Sender.NotNull(), w.NotNull().EventArgs.NotNull()))
                    .AddTo(disposable);
                
                adapter.GetSwipeLeftAction()
                    .InvokeCommand(ViewModel.MessageItemViewModel.ChangeReadItemCommand)
                    .AddTo(disposable);
                
                adapter.GetSwipeRightAction()
                    .InvokeCommand(ViewModel.MessageItemViewModel.ChangeFavoriteCommand)
                    .AddTo(disposable);
                
                _viewHolder.RefreshLayout.GetRefreshAction()
                    .SelectUnit()
                    .InvokeCommand(ViewModel.RefreshCommand)
                    .AddTo(disposable);
                
                ViewModel.RefreshCommand.IsExecuting
                    .BindTo(_viewHolder.RefreshLayout, refreshLayout => refreshLayout.Refreshing)
                    .AddTo(disposable);
                
                ViewModel.ListViewModel.WhenAnyValue(w => w.IsEmpty)
                    .Select(w => w.ToVisibility())
                    .BindTo(_viewHolder.EmptyTextView, textView => textView.Visibility)
                    .AddTo(disposable);

                ViewModel.LoadCommand.ExecuteIfCan();
            });
            
            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater) { inflater.Inflate(Resource.Menu.menu_rssDetail, menu); }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var rssModel = ViewModel.Parameters.RssFeedModel;
            switch (item.ItemId)
            {
                case Resource.Id.menuItem_rssDetail_remove:
                    ViewModel.RssFeedItemViewModel.ShowDeleteDialogCommand.ExecuteIfCan(rssModel);
                    break;
                case Resource.Id.menuItem_rssDetail_edit:
                    ViewModel.RssFeedItemViewModel.OpenEditItemCommand.ExecuteIfCan(rssModel);
                    break;
                case Resource.Id.menuItem_rssDetail_share:
                    ViewModel.RssFeedItemViewModel.ShareCommand.ExecuteIfCan(rssModel);
                    break;
                case Resource.Id.menuItem_rssDetail_readAllMessages:
                    ViewModel.RssFeedItemViewModel.ReadAllMessagesCommand.ExecuteIfCan(rssModel);
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
                    ViewModel.MessageItemViewModel.ShareItemCommand.ExecuteIfCan(model);
                    break;
                case Resource.Id.menuItem_rssDetailList_contextRead:
                    ViewModel.MessageItemViewModel.ChangeReadItemCommand.ExecuteIfCan(model);
                    break;
                case Resource.Id.menuItem_rssDetailList_contextFavorite:
                    ViewModel.MessageItemViewModel.ChangeFavoriteCommand.ExecuteIfCan(model);
                    break;
            }
        }
    }
}
