using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Droid.Container;
using Droid.Repository.Configuration;
using Droid.Screens.Base.SwipeRecyclerView;
using Droid.Screens.Navigation;
using Droid.Screens.RssList;
using Shared.Api;
using Shared.Configuration.Settings;

namespace Droid.Screens.FeedlySearch
{
    public class FeedlySearchFragment : TitleFragment
    {
        [Inject]
        private IFeedlyCloudApiClient _feedlyCloudApiClient;

        [Inject] private IConfigurationRepository _configurationRepository;
        
        protected override int LayoutId => Resource.Layout.fragment_feedly_search;
        public override bool RootFragment => true;

        public FeedlySearchFragment()
        {
            
        }
        
        protected override void RestoreState(Bundle saved)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            HasOptionsMenu = true;
            
            var recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_feedlySearch_list);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Context, LinearLayoutManager.Vertical, false));
            recyclerView.AddItemDecoration(new DividerItemDecoration(Context, DividerItemDecoration.Vertical));
            
            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.menu_feedlySearch, menu);

            var searchItem = menu?.FindItem(Resource.Id.menuItem_feedlySearch_search);
            var actionView = searchItem.ActionView as SearchView;
            actionView.QueryTextChange += async (sender, args) =>
            {
                var text = args.NewText;
                var configuration = _configurationRepository.GetSettings<AppConfiguration>();
                var recyclerView = View.FindViewById<RecyclerView>(Resource.Id.recyclerView_feedlySearch_list);

                FeedlyRssAdapter adapter;
                
                if (!string.IsNullOrEmpty(text))
                {
                    var result = await _feedlyCloudApiClient.FindByQuery(text);
            
                    adapter = new FeedlyRssAdapter(result.Results.ToList(), Activity, configuration);
                }
                else
                {
                    adapter = new FeedlyRssAdapter(new List<FeedlyRss>(), Activity, configuration);
                }
                
                recyclerView.SetAdapter(adapter);
                adapter.NotifyDataSetChanged();   
            };
            
            base.OnCreateOptionsMenu(menu, inflater);
        }
    }
}