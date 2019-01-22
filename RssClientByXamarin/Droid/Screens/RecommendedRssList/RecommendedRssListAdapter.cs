using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.Screens.Base.Adapters;
using Droid.Screens.RecommendedCategoryList;
using FFImageLoading;
using FFImageLoading.Work;
using Realms;
using Shared;
using Shared.Database.Rss;
using Shared.Services.Navigator;
using Shared.ViewModels;
using Xamarin.Essentials;

namespace Droid.Screens.RecommendedRssList
{
    public class RecommendedRssListAdapter : WithItemsAdapter<RssRecommendationModel, IQueryable<RssRecommendationModel>>
    {
        public RecommendedRssListAdapter(IQueryable<RssRecommendationModel> items, Activity activity) : base(items, activity)
        {
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is RssRecommendedItemViewHolder itemViewHolder)
            {
                var item = Items.ElementAt(position) as RssRecommendationModel;
                itemViewHolder.TitleView.Text = item.Rss;
                
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

            view.Click += (sender, args) =>
            {
                var navigator = App.Container.Resolve<INavigator>();
                var way = App.Container.Resolve<IWay<RecommendedCategoryListViewModel, RecommendedCategoryListViewModel.Way>>();
                navigator.Go(way);
            };
            
            return new RssRecommendedItemViewHolder(view);
        }
    }
}