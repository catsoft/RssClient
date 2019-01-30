using System.Globalization;
using System.Linq;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.Screens.Base.SwipeRecyclerView;
using FFImageLoading;
using FFImageLoading.Work;
using RssClient.Repository;
using Shared;
using Shared.Database.Rss;
using Shared.Repository;
using Shared.Services.Locale;
using Shared.Services.Navigator;
using Shared.ViewModels;
using Xamarin.Essentials;

namespace Droid.Screens.RssList
{
    public class RssListAdapter : SwipeListAdapter<RssModel, IQueryable<RssModel>>
    {
        private readonly IRssRepository _rssRepository;
        private readonly IRssMessagesRepository _rssMessagesRepository;
        private readonly INavigator _navigator;

        public override void OnItemDismiss(int position)
        {
            var item = Items.ElementAt(position);
            _rssRepository.Remove(item);
            NotifyItemRemoved(position);
        }

        public RssListAdapter(IQueryable<RssModel> items, Activity activity) : base(items, activity)
        {
            _rssRepository = App.Container.Resolve<IRssRepository>();
            _rssMessagesRepository = App.Container.Resolve<IRssMessagesRepository>();
            _navigator = App.Container.Resolve<INavigator>();
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = Items.ElementAt(position);

            if (holder is RssListViewHolder rssListViewHolder)
            {
                var localeService = App.Container.Resolve<ILocale>();

                rssListViewHolder.TitleTextView.Text = item.Name;
                rssListViewHolder.SubtitleTextView.Text = item.UpdateTime == null
                    ? Activity.GetText(Resource.String.rssList_notUpdated)
                    : $"{Activity.GetText(Resource.String.rssList_updated)} {item.UpdateTime.Value.ToString("g", new CultureInfo(localeService.GetCurrentLocaleId()))}";
                rssListViewHolder.Item = item;
                rssListViewHolder.CountTextView.Text = _rssMessagesRepository.GetCountForModel(item).ToString();

                ImageService.Instance.LoadUrl(item.UrlPreviewImage)
                    .LoadingPlaceholder("no_image.png", ImageSource.CompiledResource)
                    .ErrorPlaceholder("no_image.png", ImageSource.CompiledResource)
                    .Into(rssListViewHolder.IconView);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_rss, parent, false);
            var holder = new RssListViewHolder(view);

            holder.ClickView.Click += (sender, args) => { OpenDetailActivity(holder.Item); };
            holder.ClickView.LongClick += (sender, args) => { ItemLongClick(holder.Item, sender); };

            return holder;
        }

        private void ItemLongClick(RssModel holderItem, object sender)
        {
            var menu = new PopupMenu(Activity, sender as View, (int) GravityFlags.Right);
            menu.MenuItemClick += (o, eventArgs) => MenuClick(holderItem, eventArgs);
            menu.Inflate(Resource.Menu.contextMenu_rssList);
            menu.Show();
        }

        private void MenuClick(RssModel holderItem, PopupMenu.MenuItemClickEventArgs eventArgs)
        {
            switch (eventArgs.Item.ItemId)
            {
                case Resource.Id.menuItem_rssList_contextEdit:
                    EditItem(holderItem);
                    break;
                case Resource.Id.menuItem_rssList_contextRemove:
                    DeleteItem(holderItem);
                    break;

                case Resource.Id.menuItem_rssList_contextShare:
                    ShareItem(holderItem);
                    break;
            }
        }

        private async void ShareItem(RssModel holderItem)
        {
            await Share.RequestAsync(holderItem.Rss);
        }

        private void EditItem(RssModel holderItem)
        {
            var navigator = App.Container.Resolve<INavigator>();
            var editWay = App.Container.Resolve<RssEditViewModel.Way>();
            editWay.Data = new RssEditViewModel.Way.WayData(holderItem.Id);
            navigator.Go(editWay);
        }

        private void DeleteItem(RssModel holderItem)
        {
            var builder = new AlertDialog.Builder(Activity);
            builder.SetPositiveButton(Activity.GetText(Resource.String.rssDeleteDialog_positiveTitle),
                (sender, args) => { _rssRepository.Remove(holderItem); });
            builder.SetNegativeButton(Activity.GetText(Resource.String.rssDeleteDialog_negativeTitle),
                (sender, args) => { });
            builder.SetTitle(Activity.GetText(Resource.String.rssDeleteDialog_Title));
            builder.Show();
        }

        private void OpenDetailActivity(RssModel holderItem)
        {
            var way = App.Container.Resolve<RssItemDetailViewModel.Way>();
            way.Data = new RssItemDetailViewModel.Way.WayData(holderItem);
            _navigator.Go(way);
        }
    }
}