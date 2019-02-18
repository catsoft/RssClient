using System.Collections.Generic;
using Android.App;
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
    public abstract class BaseRssMessageAdapter<TCollection, TViewHolder> : DataBindAdapter<RssMessageData, TCollection, TViewHolder>
        where TCollection : IEnumerable<RssMessageData>
        where TViewHolder : RecyclerView.ViewHolder, IDataBind<RssMessageData>
    {
        private readonly IRssMessagesRepository _rssMessagesRepository;
        
        protected BaseRssMessageAdapter(TCollection items, Activity activity, IRssMessagesRepository rssMessagesRepository) : base(items, activity)
        {
            _rssMessagesRepository = rssMessagesRepository;
        }
        
        protected void InFavoriteItem(RssMessageData holderItem)
        {
            _rssMessagesRepository.ChangeIsFavorite(holderItem.Id);
        }

        protected void ReadItem(RssMessageData holderItem)
        {
            _rssMessagesRepository.ChangeIsRead(holderItem.Id);
        }
        
        protected void ItemLongClick(RssMessageData holderItem, object sender)
        {
            var menu = new PopupMenu(Activity, sender as View, (int) GravityFlags.Right);
            menu.MenuItemClick += (o, eventArgs) => MenuClick(holderItem, eventArgs);
            menu.Inflate(Resource.Menu.contextMenu_rssDetailList);
            menu.Show();
        }

        protected void MenuClick(RssMessageData holderItem, PopupMenu.MenuItemClickEventArgs eventArgs)
        {
            switch (eventArgs.Item.ItemId)
            {
                case Resource.Id.menuItem_rssDetailList_contextShare:
                    ShareItem(holderItem);
                    break;
                case Resource.Id.menuItem_rssDetailList_contextRead:
                    ReadItem(holderItem);
                    break;
                case Resource.Id.menuItem_rssDetailList_contextFavorite:
                    InFavoriteItem(holderItem);
                    break;
            }
        }

        protected async void ShareItem(RssMessageData holderItem)
        {
            await Share.RequestAsync(holderItem.Url);
        }

        protected void OpenContentActivity(RssMessageData item)
        {
            var navigator = App.Container.Resolve<INavigator>();
            var way = App.Container.Resolve<RssMessageViewModel.Way>();
            way.Data = new RssMessageViewModel.Way.WayData(item);
            navigator.Go(way);
        }
    }
}