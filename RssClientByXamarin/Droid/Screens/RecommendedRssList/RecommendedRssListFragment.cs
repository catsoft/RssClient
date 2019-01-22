using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Autofac;
using Droid.Repository;
using Droid.Screens.Base.Adapters;
using Droid.Screens.Navigation;
using Shared;
using Shared.Database.Rss;

namespace Droid.Screens.RecommendedRssList
{
    public class RecommendedRssListFragment : TitleFragment
    {
        protected override int LayoutId => Resource.Layout.fragment_recommended;
        public override bool RootFragment => true;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Title = GetText(Resource.String.recommended_title);

            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var list = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_rssRecommendedList_list);
            list.SetLayoutManager(new LinearLayoutManager(Context, LinearLayoutManager.Vertical, false));
            list.AddItemDecoration(new DividerItemDecoration(Context, DividerItemDecoration.Vertical));

            var repository = App.Container.Resolve<IRssRecommendedRepository>();
            var items = repository.GetAll();
            var adapter = new RecommendedRssListAdapter(items, Activity);
            list.SetAdapter(adapter);

            return view;
        }
    }
}