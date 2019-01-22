using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.Repository;
using Droid.Screens.Base.Adapters;
using Droid.Screens.Navigation;
using Droid.Screens.RecommendedRssList;
using Java.Util;
using Shared;
using Shared.Database.Rss;

namespace Droid.Screens.RecommendedCategoryList
{
    public class RecommendedCategoryListFragment : TitleFragment
    {
        protected override int LayoutId => Resource.Layout.fragment_recommended_category_list;
        public override bool RootFragment => true;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var list = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_rssRecommendedCategoryList_list);
            list.SetLayoutManager(new LinearLayoutManager(Context, LinearLayoutManager.Vertical, false));
            list.AddItemDecoration(new DividerItemDecoration(Context, DividerItemDecoration.Vertical));

            var repository = App.Container.Resolve<IRssRecommendedRepository>();
            var items = repository.GetCategories();
            var adapter = new RecommendedCategoriesRssListAdapter(items, Activity);
            list.SetAdapter(adapter);
            
            return view;
        }
    }

    public class RecommendedCategoriesRssListAdapter : WithItemsAdapter<Categories, IQueryable<Categories>>
    {
        public RecommendedCategoriesRssListAdapter(IQueryable<Categories> items, Activity activity) : base(items, activity)
        {
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is RssRecommendedCategoryViewHolder recommendedHeaderViewHolder)
            {
                var item = Items.ElementAt(position);
                recommendedHeaderViewHolder.TitleView.Text = item.ToString();
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_recommended_category_rss, parent,false);
            
            return new RssRecommendedCategoryViewHolder(view);
        }
    }
}