using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Droid.Container;
using Droid.Screens.Navigation;
using Shared.Repository.RssRecommended;

namespace Droid.Screens.RecommendedCategoryList
{
    public class RecommendedCategoryListFragment : TitleFragment
    {
        [Inject] private IRssRecommendedRepository _repository;

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
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            Title = GetText(Resource.String.recommended_title);
            
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