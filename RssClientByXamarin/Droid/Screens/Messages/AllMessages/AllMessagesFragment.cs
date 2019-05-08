using System;
using System.Reactive.Linq;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Core.Extensions;
using Core.Services.RssMessages;
using Core.ViewModels.Messages.AllMessages;
using Droid.Infrastructure.Collections;
using Droid.NativeExtension;
using Droid.NativeExtension.Events;
using Droid.Screens.Base.SwipeButtonRecyclerView;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;

namespace Droid.Screens.Messages.AllMessages
{
    public class AllMessagesFragment : BaseFragment<AllMessagesViewModel>
    {
        // ReSharper disable once NotNullMemberIsNotInitialized
        [NotNull] private AllMessagesFragmentViewHolder _viewHolder;
        
        protected override int LayoutId => Resource.Layout.fragment_all_messages_list;
        public override bool IsRoot => true;

        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState).NotNull();

            _viewHolder = new AllMessagesFragmentViewHolder(view);

            Title = Activity.GetText(Resource.String.rssList_title);

            HasOptionsMenu = true;

            var adapter = new AllMessagesListAdapter(Activity, ViewModel.AppConfiguration);
            _viewHolder.RecyclerView.SetAdapter(adapter);

            var callback = new SwipeButtonTouchHelperCallback();
            var helper = new ItemTouchHelper(callback);
            helper.AttachToRecyclerView(_viewHolder.RecyclerView);

            var adapterUpdater = new AdapterUpdater<RssMessageServiceModel>(_viewHolder.RecyclerView, adapter, ViewModel.ListViewModel.SourceList);

            OnActivation(disposable =>
            {
                ViewModel.ListViewModel.ConnectChanges
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(w => adapterUpdater.Update(w))
                    .AddTo(disposable);

                this.BindCommand(ViewModel, model => model.OpenCreateScreenCommand, fragment => fragment._viewHolder.FloatingActionButton)
                    .AddTo(disposable);
                
                this.BindCommand(ViewModel, model => model.ReadAllMessagesCommand, fragment => fragment._viewHolder.ReadAllFloatingActionButton)
                    .AddTo(disposable);
                
                ViewModel.ReadAllMessagesCommand.CanExecute
                    .Select(w => w.ToVisibility())
                    .BindTo(_viewHolder.ReadAllFloatingActionButton, fab => fab.Visibility)
                    .AddTo(disposable);
                
                adapter.GetClickAction()
                    .InvokeCommand(ViewModel.MessageItemViewModel.OpenContentScreenCommand)
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
                
                ViewModel.WhenAnyValue(w => w.ListViewModel.IsEmpty)
                    .Select(w => w.ToVisibility())
                    .BindTo(_viewHolder.EmptyTextView, textView => textView.Visibility)
                    .AddTo(disposable);
                
                ViewModel.RssFeedsUpdaterViewModel.UpdateCommand.IsExecuting
                    .Select(w => w.ToVisibility())
                    .BindTo(_viewHolder.TopProgressBar, progress => progress.Visibility)
                    .AddTo(disposable);
                
                ViewModel.LoadRssMessagesCommand.ExecuteIfCan().AddTo(disposable);
                
                ViewModel.RssFeedsUpdaterViewModel.SoftUpdateCommand.ExecuteIfCan().AddTo(disposable);
            });

            return view;
        }

        public override void OnCreateOptionsMenu([NotNull] IMenu menu, [NotNull] MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.menu_rssAllMessageList, menu);
        }

        public override bool OnOptionsItemSelected([NotNull] IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuItem_rssAllMessageList_change:
                    ViewModel.OpenRssListScreenCommand.ExecuteIfCan();
                    break;
                case Resource.Id.menuItem_rssAllMessageList_filter:
                    ViewModel.OpenRssAllMessagesFilterScreenCommand.ExecuteIfCan();
                    break;
                
                case Resource.Id.menuItem_rssAllMessagesList_refresh:
                    ViewModel.RssFeedsUpdaterViewModel.HardUpdateCommand.ExecuteIfCan();
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
