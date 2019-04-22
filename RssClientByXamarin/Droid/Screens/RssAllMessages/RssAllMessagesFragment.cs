using System;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Droid.Infrastructure.Collections;
using Droid.NativeExtension;
using Droid.Screens.Base.SwipeButtonRecyclerView;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;
using Shared.Database.Rss;
using Shared.Extensions;
using Shared.ViewModels.RssAllMessages;

namespace Droid.Screens.RssAllMessages
{
    public class RssAllMessagesFragment : BaseFragment<RssAllMessagesViewModel>
    {
        // ReSharper disable once NotNullMemberIsNotInitialized
        [NotNull] private RssAllMessagesFragmentViewHolder _viewHolder;
        
        protected override int LayoutId => Resource.Layout.fragment_all_messages_list;
        public override bool IsRoot => true;

        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState).NotNull();

            _viewHolder = new RssAllMessagesFragmentViewHolder(view);

            Title = Activity.GetText(Resource.String.rssList_title);

            HasOptionsMenu = true;

            var adapter = new RssAllMessagesListAdapter(Activity, ViewModel.AppConfiguration);
            _viewHolder.RecyclerView.SetAdapter(adapter);

            var callback = new SwipeButtonTouchHelperCallback();
            var helper = new ItemTouchHelper(callback);
            helper.AttachToRecyclerView(_viewHolder.RecyclerView);

            var adapterUpdater = new AdapterUpdater<RssMessageServiceModel>(adapter, ViewModel.SourceList);

            OnActivation(disposable =>
            {
                ViewModel.ConnectChanges
                    .Subscribe(w => adapterUpdater.Update(w))
                    .AddTo(disposable);

                _viewHolder.FloatingActionButton.Events()
                    .NotNull()
                    .Click
                    .NotNull()
                    .InvokeCommand(ViewModel.OpenCreateScreenCommand)
                    .AddTo(disposable);
                
                adapter.GetRssMessageItemClickEvent()
                    .InvokeCommand(ViewModel.OpenContentScreenCommand)
                    .AddTo(disposable);

                adapter.GetRssMessageItemLongClickEvent()
                    .Subscribe(w => ItemLongClick(w.NotNull().Sender.NotNull(), w.NotNull().EventArgs.NotNull()))
                    .AddTo(disposable);
                
                adapter.GetRssMessageLeftButtonClickEvent()
                    .InvokeCommand(ViewModel.ReadItemCommand)
                    .AddTo(disposable);
                
                adapter.GetRssMessageRightButtonClickEvent()
                    .InvokeCommand(ViewModel.InFavoriteCommand)
                    .AddTo(disposable);
                
                ViewModel.LoadRssMessagesCommand.Execute().NotNull().Subscribe().AddTo(disposable);
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
                    ViewModel.OpenRssListScreenCommand.Execute()?.Subscribe();
                    break;
                case Resource.Id.menuItem_rssAllMessageList_filter:
                    ViewModel.OpenRssAllMessagesFilterScreenCommand.Execute()?.Subscribe();
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
                    ViewModel.ShareItemCommand.Execute(model).NotNull().Subscribe();
                    break;
                case Resource.Id.menuItem_rssDetailList_contextRead:
                    ViewModel.ReadItemCommand.Execute(model).NotNull().Subscribe();
                    break;
                case Resource.Id.menuItem_rssDetailList_contextFavorite:
                    ViewModel.InFavoriteCommand.Execute(model).NotNull().Subscribe();
                    break;
            }
        }
    }
}
