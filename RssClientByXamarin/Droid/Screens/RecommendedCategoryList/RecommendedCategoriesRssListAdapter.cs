using System.Collections.Generic;
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
    public class RecommendedCategoriesRssListAdapter : DataBindAdapter<Categories, IEnumerable<Categories>, RssRecommendedCategoryViewHolder>
    {
        public RecommendedCategoriesRssListAdapter(IEnumerable<Categories> items, Activity activity) : base(items, activity)
        {
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_recommended_category_rss, parent, false);

            var viewHolder = new RssRecommendedCategoryViewHolder(view);

            view.Click += (sender, args) =>
            {
                var navigator = App.Container.Resolve<INavigator>();
                var way = App.Container.Resolve<RecommendedViewModel.Way>();
                way.Data = new RecommendedViewModel.Way.WayData(viewHolder.Item);
                navigator.Go(way);
            };

            return viewHolder;
        }
    }
}