using System;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Droid.Infrastructure.Collections;
using Droid.NativeExtension;
using Droid.Screens.Base.SwipeButtonRecyclerView;
using Droid.Screens.Navigation;
using Droid.Screens.RssAllMessages;
using JetBrains.Annotations;
using ReactiveUI;
using Shared.Database.Rss;
using Shared.Extensions;
using Shared.ViewModels.RssFavoriteMessages;

namespace Droid.Screens.RssFavoriteMessagesList
{
    public class RssFavoriteMessagesListFragment : BaseFragment<RssFavoriteMessagesViewModel>
    {
        private RssFavoriteMessagesListFragmentViewHolder _viewHolder;
        
        protected override int LayoutId => Resource.Layout.fragment_favorite_messages_list;
        public override bool IsRoot => true;

        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState).NotNull();

            _viewHolder = new RssFavoriteMessagesListFragmentViewHolder(view);
            
            Title = Activity.GetText(Resource.String.rssFavoriteMessages_title);

            var adapter = new RssAllMessagesListAdapter(Activity, ViewModel.AppConfiguration);
            _viewHolder.RecyclerView.SetAdapter(adapter);

            var callback = new SwipeButtonTouchHelperCallback();
            var helper = new ItemTouchHelper(callback);
            helper.AttachToRecyclerView(_viewHolder.RecyclerView);

            var adapterUpdater = new AdapterUpdater<RssMessageServiceModel>(_viewHolder.RecyclerView, adapter, ViewModel.ListViewModel.SourceList);
            
            OnActivation(disposable =>
            {
                ViewModel.ListViewModel.ConnectChanges
                    .Subscribe(w => adapterUpdater.Update(w))
                    .AddTo(disposable);
                
                adapter.GetClickAction()
                    .InvokeCommand(ViewModel.RssMessageViewModel.OpenContentScreenCommand)
                    .AddTo(disposable);

                adapter.GetLongClickAction()
                    .Subscribe(w => ItemLongClick(w.NotNull().Sender.NotNull(), w.NotNull().EventArgs.NotNull()))
                    .AddTo(disposable);
                
                adapter.GetSwipeLeftAction()
                    .InvokeCommand(ViewModel.RssMessageViewModel.ChangeReadItemCommand)
                    .AddTo(disposable);
                
                adapter.GetSwipeRightAction()
                    .InvokeCommand(ViewModel.RssMessageViewModel.ChangeFavoriteCommand)
                    .AddTo(disposable);
                
                ViewModel.LoadCommand.Execute().NotNull().Subscribe().AddTo(disposable);
            });
            
            return view;
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
                    ViewModel.RssMessageViewModel.ShareItemCommand.Execute(model).NotNull().Subscribe();
                    break;
                case Resource.Id.menuItem_rssDetailList_contextRead:
                    ViewModel.RssMessageViewModel.ChangeReadItemCommand.Execute(model).NotNull().Subscribe();
                    break;
                case Resource.Id.menuItem_rssDetailList_contextFavorite:
                    ViewModel.RssMessageViewModel.ChangeFavoriteCommand.Execute(model).NotNull().Subscribe();
                    break;
            }
        }
    }
}
