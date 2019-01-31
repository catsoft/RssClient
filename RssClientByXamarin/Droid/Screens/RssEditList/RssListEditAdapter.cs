using System.Globalization;
using System.Linq;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.Screens.Base.Adapters;
using FFImageLoading;
using FFImageLoading.Work;
using Shared;
using Shared.Database.Rss;
using Shared.Services.Locale;

namespace Droid.Screens.RssEditList
{
    public class RssListEditAdapter : WithItemsAdapter<RssModel, IQueryable<RssModel>>
    {
        public RssListEditAdapter(IQueryable<RssModel> items, Activity activity) : base(items, activity)
        {
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is RssListEditViewHolder rssListEditViewHolder)
            {
                var item = Items.ElementAt(position);
                
                var localeService = App.Container.Resolve<ILocale>();

                rssListEditViewHolder.TitleTextView.Text = item.Name;
                rssListEditViewHolder.SubtitleTextView.Text = item.UpdateTime == null
                    ? Activity.GetText(Resource.String.rssList_notUpdated)
                    : $"{Activity.GetText(Resource.String.rssList_updated)} {item.UpdateTime.Value.ToString("g", new CultureInfo(localeService.GetCurrentLocaleId()))}";
                rssListEditViewHolder.Item = item;

                ImageService.Instance.LoadUrl(item.UrlPreviewImage)
                    .LoadingPlaceholder("no_image.png", ImageSource.CompiledResource)
                    .ErrorPlaceholder("no_image.png", ImageSource.CompiledResource)
                    .Into(rssListEditViewHolder.IconView);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_rss_edit, parent, false);

            return new RssListEditViewHolder(view);
        }
    }
}