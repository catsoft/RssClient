using System.Globalization;
using System.Linq;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.NativeExtension;
using Droid.Screens.Base.Adapters;
using FFImageLoading;
using FFImageLoading.Work;
using Shared;
using Shared.Database.Rss;
using Shared.Services.Locale;
using Shared.ViewModels;

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
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_rss_edit, parent, false);

            var viewHolder = new RssListEditViewHolder(view);

            viewHolder.DeleteImage.Click += (sender, args) =>
            {
                Activity.Toast("DeleteImage");
            };

            viewHolder.ReorderImage.Click += (sender, args) =>
            {
                Activity.Toast("ReorderImage");
            };
            
            return viewHolder;
        }
    }
}