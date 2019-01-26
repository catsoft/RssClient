using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.Container;
using Droid.Repository;
using Droid.Screens.Navigation;
using Droid.Services.Helpers;
using Shared;
using Shared.Database.Rss;

namespace Droid.Screens.RecommendedRssList
{
    public class RecommendedRssListFragment : TitleFragment
    {
        [Inject]
        private IRssRecommendedRepository _repository;        
        
        private Categories _categories;

        protected override int LayoutId => Resource.Layout.fragment_recommended;
        public override bool RootFragment => false;

        public RecommendedRssListFragment()
        {
            
        }

        public RecommendedRssListFragment(Categories categories)
        {
            _categories = categories;
        }
        
        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            
            outState.PutInt(nameof(_categories), (int)_categories);
        }

        
        protected override void RestoreState(Bundle saved)
        {
            _categories = (Categories)saved.GetInt(nameof(_categories));
        }
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            
            Title = _categories.ToLocaleString(Context);

            var list = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_rssRecommendedList_list);
            list.SetLayoutManager(new LinearLayoutManager(Context, LinearLayoutManager.Vertical, false));
            list.AddItemDecoration(new DividerItemDecoration(Context, DividerItemDecoration.Vertical));

            var items = _repository.GetAllByCategory(_categories);
            var adapter = new RecommendedRssListAdapter(items, Activity);
            list.SetAdapter(adapter);

            return view;
        }
    }
}