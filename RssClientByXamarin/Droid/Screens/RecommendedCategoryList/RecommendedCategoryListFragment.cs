using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.Container;
using Droid.Repository;
using Droid.Screens.Navigation;
using Droid.Screens.RecommendedRssList;
using Java.Util;
using Shared;

namespace Droid.Screens.RecommendedCategoryList
{
    public class RecommendedCategoryListFragment : TitleFragment
    {
        [Inject]
        private IRssRecommendedRepository _repository;

        protected override int LayoutId => Resource.Layout.fragment_recommended_category_list;
        public override bool RootFragment => true;

        public RecommendedCategoryListFragment()
        {
            
        }
        
        protected override void RestoreState(Bundle saved)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Title = GetText(Resource.String.recommended_title);
            
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var list = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_rssRecommendedCategoryList_list);
            list.SetLayoutManager(new LinearLayoutManager(Context, LinearLayoutManager.Vertical, false));
            list.AddItemDecoration(new DividerItemDecoration(Context, DividerItemDecoration.Vertical));

            var items = _repository.GetCategories();
            var adapter = new RecommendedCategoriesRssListAdapter(items, Activity);
            list.SetAdapter(adapter);
            
            return view;
        }
    }
}