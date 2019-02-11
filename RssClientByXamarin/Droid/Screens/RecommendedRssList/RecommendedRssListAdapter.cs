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
using Shared.Configuration;
using Shared.Database.Rss;

namespace Droid.Screens.RecommendedRssList
{
    public class RecommendedRssListAdapter : DataBindAdapter<RssRecommendationModel, IQueryable<RssRecommendationModel>, RssRecommendedViewHolder>
    {
        private readonly AppConfiguration _appConfiguration;
        
        public RecommendedRssListAdapter(IQueryable<RssRecommendationModel> items, Activity activity, AppConfiguration appConfiguration) : base(items, activity)
        {
            _appConfiguration = appConfiguration;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var inflater = LayoutInflater.From(parent.Context);

            var view = inflater.Inflate(Resource.Layout.list_item_recommended_rss, parent, false);

            var viewHolder = new RssRecommendedViewHolder(view, _appConfiguration.LoadAndShowImages);

            viewHolder.AddImageView.Click += (sender, args) =>
            {
                var rssRepository = App.Container.Resolve<IRssRepository>();
                rssRepository.InsertByUrl(viewHolder.Item.Rss);
                Activity.Toast(Activity.GetText(Resource.String.recommended_rss_add_rss_toast) + viewHolder.Item.Rss);
            };

            return viewHolder;
        }
    }
}