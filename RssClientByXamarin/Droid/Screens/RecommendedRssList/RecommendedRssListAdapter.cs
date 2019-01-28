using System;
using System.Linq;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.NativeExtension;
using Droid.Screens.Base.Adapters;
using FFImageLoading;
using FFImageLoading.Work;
using RssClient.Repository;
using Shared;
using Shared.Database.Rss;

namespace Droid.Screens.RecommendedRssList
{
    public class RecommendedRssListAdapter : WithItemsAdapter<RssRecommendationModel, IQueryable<RssRecommendationModel>>
    {
        public RecommendedRssListAdapter(IQueryable<RssRecommendationModel> items, Activity activity) : base(items, activity)
        {
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is RssRecommendedViewHolder itemViewHolder)
            {
                var item = Items.ElementAt(position);
                itemViewHolder.TitleView.Text = item.Rss;
                itemViewHolder.RssUrl = item.Rss;
                
                var uri = new Uri(item.Rss);
                var favicon = $"{uri.Scheme}://{uri.Host}/favicon.ico";
                
                ImageService.Instance.LoadUrl(favicon)
                    .LoadingPlaceholder("no_image.png", ImageSource.CompiledResource)
                    .ErrorPlaceholder("no_image.png", ImageSource.CompiledResource)
                    .Into(itemViewHolder.RssIcon);
            }
        }
        
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var inflater = LayoutInflater.From(parent.Context);
            
            var view = inflater.Inflate(Resource.Layout.list_item_recommended_rss, parent, false);

            var viewHolder = new RssRecommendedViewHolder(view);

            viewHolder.AddImageView.Click += (sender, args) =>
            {
                var rssRepository = App.Container.Resolve<IRssRepository>();
                rssRepository.InsertByUrl(viewHolder.RssUrl);
                Activity.Toast(Activity.GetText(Resource.String.recommended_rss_add_rss_toast) + viewHolder.RssUrl);
            };
            
            return viewHolder;
        }
    }
}