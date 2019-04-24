using System;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Core.Extensions;
using Core.Services.RssMessages;
using Core.ViewModels.Messages.FavoriteMessages;
using Droid.Infrastructure.Collections;
using Droid.NativeExtension;
using Droid.NativeExtension.Events;
using Droid.Screens.Base.SwipeButtonRecyclerView;
using Droid.Screens.Messages.AllMessages;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;

namespace Droid.Screens.Messages.FavoriteMessagesList
{
    public class FavoriteMessagesListFragment : BaseFragment<FavoriteMessagesViewModel>
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

            var adapter = new AllMessagesListAdapter(Activity, ViewModel.AppConfiguration);
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
                    .InvokeCommand(ViewModel.RssMessageItemViewModel.OpenContentScreenCommand)
                    .AddTo(disposable);

                adapter.GetLongClickAction()
                    .Subscribe(w => ItemLongClick(w.NotNull().Sender.NotNull(), w.NotNull().EventArgs.NotNull()))
                    .AddTo(disposable);
                
                adapter.GetSwipeLeftAction()
                    .InvokeCommand(ViewModel.RssMessageItemViewModel.ChangeReadItemCommand)
                    .AddTo(disposable);
                
                adapter.GetSwipeRightAction()
                    .InvokeCommand(ViewModel.RssMessageItemViewModel.ChangeFavoriteCommand)
                    .AddTo(disposable);
                
                ViewModel.ListViewModel.WhenAnyValue(model => model.IsEmpty)
                    .Subscribe(w => _viewHolder.EmptyTextView.Visibility = w.ToVisibility())
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
                    ViewModel.RssMessageItemViewModel.ShareItemCommand.Execute(model).NotNull().Subscribe();
                    break;
                case Resource.Id.menuItem_rssDetailList_contextRead:
                    ViewModel.RssMessageItemViewModel.ChangeReadItemCommand.Execute(model).NotNull().Subscribe();
                    break;
                case Resource.Id.menuItem_rssDetailList_contextFavorite:
                    ViewModel.RssMessageItemViewModel.ChangeFavoriteCommand.Execute(model).NotNull().Subscribe();
                    break;
            }
        }
    }
}
