using System.Globalization;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Com.Bumptech.Glide;
using Droid.Screens.Base.Adapters;
using Droid.Screens.Rss.Detail;
using Droid.Screens.Rss.Edit;
using RssClient.Repository;
using Shared;
using Shared.Database.Rss;
using Shared.Repository;
using Shared.Services.Locale;

namespace Droid.Screens.Rss.List.RssList
{
	public class RssListAdapter : WithItemsAdapter<RssModel, IQueryable<RssModel>>
    {
	    private readonly IRssRepository _rssRepository;
        private readonly IRssMessagesRepository _rssMessagesRepository;

        public RssListAdapter(IQueryable<RssModel> items, Activity activity) : base(items, activity)
        {
			_rssRepository = App.Container.Resolve<IRssRepository>();
            _rssMessagesRepository = App.Container.Resolve<IRssMessagesRepository>();
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
                    : $"{Activity.GetText(Resource.String.rssList_updated)}{item.UpdateTime.Value.ToString("g", new CultureInfo(localeService.GetCurrentLocaleId()))}";
                rssListViewHolder.Item = item;
                rssListViewHolder.CountTextView.Text = _rssMessagesRepository.GetCountForModel(item).ToString();
                var placeHolder = ContextCompat.GetDrawable(Activity, Resource.Drawable.no_image);
                placeHolder.SetColorFilter(Color.Orange, PorterDuff.Mode.Add);
                rssListViewHolder.IconView.SetImageDrawable(placeHolder);
                //TODO Конкретнее обработать с placeholderом как в ios
                if(!string.IsNullOrEmpty(item.UrlPreviewImage))
                    Glide.With(Activity).Load(item.UrlPreviewImage).Into(rssListViewHolder.IconView);
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

        private void ShareItem(RssModel holderItem)
        {
        }

        private void EditItem(RssModel holderItem)
        {
            var intent = RssEditActivity.Create(Activity, holderItem.Id);
            Activity.StartActivity(intent);
        }

        private void DeleteItem(RssModel holderItem)
		{
            var builder = new AlertDialog.Builder(Activity);
            builder.SetPositiveButton(Activity.GetText(Resource.String.rssDeleteDialog_positiveTitle), (sender, args) =>
            {
                _rssRepository.Remove(holderItem);
            });
            builder.SetNegativeButton(Activity.GetText(Resource.String.rssDeleteDialog_negativeTitle), (sender, args) => { });
            builder.SetTitle(Activity.GetText(Resource.String.rssDeleteDialog_Title));
            builder.Show();
        }

        private void OpenDetailActivity(RssModel holderItem)
        {
            var intent = new Intent(Activity, typeof(RssDetailActivity));
            intent.PutExtra(RssDetailActivity.ItemIntentId, holderItem.Id);
            Activity.StartActivity(intent);
        }
    }
}