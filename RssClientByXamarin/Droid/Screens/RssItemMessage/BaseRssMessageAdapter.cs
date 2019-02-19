using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.Screens.Base.Adapters;
using Shared;
using Shared.Database.Rss;
using Shared.Repository.RssMessage;
using Shared.Services.Navigator;
using Shared.ViewModels;
using Xamarin.Essentials;

namespace Droid.Screens.RssItemMessage
{
    public abstract class BaseRssMessageAdapter<TViewHolder> : DataBindAdapter<RssMessageData, List<RssMessageData>, TViewHolder>
        where TViewHolder : RecyclerView.ViewHolder, IDataBind<RssMessageData>
    {
        private readonly IRssMessagesRepository _rssMessagesRepository;
        
        protected BaseRssMessageAdapter(List<RssMessageData> items, Activity activity, IRssMessagesRepository rssMessagesRepository) : base(items, activity)
        {
            _rssMessagesRepository = rssMessagesRepository;
        }
        
        protected async void InFavoriteItem(IDataBind<RssMessageData> holder)
        {
            await _rssMessagesRepository.ChangeIsFavoriteAsync(holder.Item.Id);
            UpdateHimself(holder);
        }

        private void UpdateHimself(IDataBind<RssMessageData> holder)
        {
            var position = Items.IndexOf(holder.Item);
            var newItem = _rssMessagesRepository.FindById(holder.Item.Id);
            Items[position] = newItem;
            holder.BindData(newItem);
        }

        protected async void ReadItem(IDataBind<RssMessageData> holder)
        {
            await _rssMessagesRepository.ChangeIsReadAsync(holder.Item.Id);
            UpdateHimself(holder);
        }
        
        protected void ItemLongClick(IDataBind<RssMessageData> holder, object sender)
        {
            var menu = new PopupMenu(Activity, sender as View, (int) GravityFlags.Right);
            menu.MenuItemClick += (o, eventArgs) => MenuClick(holder, eventArgs);
            menu.Inflate(Resource.Menu.contextMenu_rssDetailList);
            menu.Show();
        }

        protected void MenuClick(IDataBind<RssMessageData> holder, PopupMenu.MenuItemClickEventArgs eventArgs)
        {
            switch (eventArgs.Item.ItemId)
            {
                case Resource.Id.menuItem_rssDetailList_contextShare:
                    ShareItem(holder);
                    break;
                case Resource.Id.menuItem_rssDetailList_contextRead:
                    ReadItem(holder);
                    break;
                case Resource.Id.menuItem_rssDetailList_contextFavorite:
                    InFavoriteItem(holder);
                    break;
            }
        }

        protected async void ShareItem(IDataBind<RssMessageData> holder)
        {
            await Share.RequestAsync(holder.Item.Url);
        }

        protected void OpenContentActivity(IDataBind<RssMessageData> holder)
        {
            var navigator = App.Container.Resolve<INavigator>();
            var way = App.Container.Resolve<RssMessageViewModel.Way>();
            way.Data = new RssMessageViewModel.Way.WayData(holder.Item);
            navigator.Go(way);
        }
    }
}