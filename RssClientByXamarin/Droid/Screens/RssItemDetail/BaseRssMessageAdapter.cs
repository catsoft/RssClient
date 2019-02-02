using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.Screens.Base.Adapters;
using Shared;
using Shared.Database.Rss;
using Shared.Repository;
using Shared.Services.Navigator;
using Shared.ViewModels;
using Xamarin.Essentials;

namespace Droid.Screens.RssItemDetail
{
    public abstract class BaseRssMessageAdapter<TCollection> : WithItemsAdapter<RssMessageModel, TCollection>
    where TCollection : IEnumerable<RssMessageModel>
    {
        private readonly IRssMessagesRepository _rssMessagesRepository;
        
        protected readonly Color BackgroundItemColor;
        protected readonly Color BackgroundItemSelectColor;
        
        protected BaseRssMessageAdapter(TCollection items, Activity activity, IRssMessagesRepository rssMessagesRepository) : base(items, activity)
        {
            _rssMessagesRepository = rssMessagesRepository;
            
            BackgroundItemColor = new Color(0, 0, 0, 0);
            BackgroundItemSelectColor = new Color(0, 0, 0, 95);
        }
        
        protected void InFavoriteItem(RssMessageModel holderItem)
        {
            _rssMessagesRepository.ChangeIsFavorite(holderItem);
        }

        protected void ReadItem(RssMessageModel holderItem)
        {
            _rssMessagesRepository.ChangeIsRead(holderItem);
        }
        
        protected void ItemLongClick(RssMessageModel holderItem, object sender)
        {
            var menu = new PopupMenu(Activity, sender as View, (int) GravityFlags.Right);
            menu.MenuItemClick += (o, eventArgs) => MenuClick(holderItem, eventArgs);
            menu.Inflate(Resource.Menu.contextMenu_rssDetailList);
            menu.Show();
        }

        protected void MenuClick(RssMessageModel holderItem, PopupMenu.MenuItemClickEventArgs eventArgs)
        {
            switch (eventArgs.Item.ItemId)
            {
                case Resource.Id.menuItem_rssDetailList_contextShare:
                    ShareItem(holderItem);
                    break;
            }
        }

        protected async void ShareItem(RssMessageModel holderItem)
        {
            await Share.RequestAsync(holderItem.Url);
        }

        protected void OpenContentActivity(RssMessageModel item)
        {
            var navigator = App.Container.Resolve<INavigator>();
            var way = App.Container.Resolve<RssMessageViewModel.Way>();
            way.Data = new RssMessageViewModel.Way.WayData(item);
            navigator.Go(way);
        }
    }
}