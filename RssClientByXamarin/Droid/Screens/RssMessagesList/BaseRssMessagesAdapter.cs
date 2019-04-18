using System.Collections.Generic;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.Screens.Base.Adapters;
using Shared;
using Shared.Infrastructure.Navigation;
using Shared.Repository.RssMessage;
using Shared.ViewModels.RssMessage;
using Xamarin.Essentials;

namespace Droid.Screens.RssMessagesList
{
    public abstract class BaseRssMessagesAdapter<TViewHolder> : DataBindAdapter<RssMessageData, List<RssMessageData>, TViewHolder>
        where TViewHolder : RecyclerView.ViewHolder, IDataBind<RssMessageData>
    {
        private readonly IRssMessagesRepository _rssMessagesRepository;
        
        protected BaseRssMessagesAdapter(List<RssMessageData> items, Activity activity, IRssMessagesRepository rssMessagesRepository) : base(items, activity)
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

        private void MenuClick(IDataBind<RssMessageData> holder, PopupMenu.MenuItemClickEventArgs eventArgs)
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

        private async void ShareItem(IDataBind<RssMessageData> holder)
        {
            await Share.RequestAsync(holder.Item.Url);
        }

        protected async void OpenContentActivity(IDataBind<RssMessageData> holder)
        {
            await _rssMessagesRepository.MarkAsReadAsync(holder.Item.Id);
            UpdateHimself(holder);
            
            var navigator = App.Container.Resolve<INavigator>();
            var parameter = new RssMessageParameterses(holder.Item);
            var typedParameter = new TypedParameter(parameter.GetType(), parameter);
            var way = App.Container.Resolve<IWayWithParameters<RssMessageViewModel, RssMessageParameterses>>(typedParameter);
            navigator.Go(way);
        }
    }
}