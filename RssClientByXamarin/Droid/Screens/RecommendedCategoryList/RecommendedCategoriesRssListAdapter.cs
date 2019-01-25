using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.Screens.Base.Adapters;
using Shared;
using Shared.Database.Rss;
using Shared.Services.Navigator;
using Shared.ViewModels;

namespace Droid.Screens.RecommendedCategoryList
{
    public class RecommendedCategoriesRssListAdapter : WithItemsAdapter<Categories, IEnumerable<Categories>>
    {
        public RecommendedCategoriesRssListAdapter(IEnumerable<Categories> items, Activity activity) : base(items, activity)
        {
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is RssRecommendedCategoryViewHolder recommendedHeaderViewHolder)
            {
                var item = Items.ElementAt(position);
                recommendedHeaderViewHolder.Categories = item;
                recommendedHeaderViewHolder.TitleView.Text = item.ToString();
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_recommended_category_rss, parent,false);

            var viewHolder = new RssRecommendedCategoryViewHolder(view);
            
            view.Click += (sender, args) =>
            {
                var navigator = App.Container.Resolve<INavigator>();
                var way = App.Container.Resolve<IWay<RecommendedViewModel, RecommendedViewModel.Way.WayData>>();
                way.Data = new RecommendedViewModel.Way.WayData(viewHolder.Categories);
                navigator.Go(way);
            };
            
            return viewHolder;
        }
    }
}