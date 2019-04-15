using System.Collections.Generic;
using System.Linq;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Droid.Screens.Navigation;
using Shared.Repository.Feedly;
using Shared.ViewModels.FeedlySearch;

namespace Droid.Screens.FeedlySearch
{
    public class FeedlySearchFragment : BaseFragment<FeedlySearchViewModel>
    {
        private FeedlySearchFragmentViewHolder _viewHolder;
        
        protected override int LayoutId => Resource.Layout.fragment_feedly_search;
        public override bool IsRoot => true;

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

            Title = GetText(Resource.String.feedly_title);
            
            _viewHolder = new FeedlySearchFragmentViewHolder(view);
            
            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.menu_feedlySearch, menu);

            var searchItem = menu?.FindItem(Resource.Id.menuItem_feedlySearch_search);

            if (searchItem.ActionView is SearchView actionView)
            {
                actionView.QueryTextChange += async (sender, args) =>
                {
                    var text = args.NewText;
                    
                    FeedlyRssAdapter adapter;

                    if (!string.IsNullOrEmpty(text))
                    {
                        var result = await ViewModel.FindByQueryAsync(text);

                        adapter = new FeedlyRssAdapter(result.ToList(), Activity, ViewModel.AppConfiguration);
                    }
                    else
                    {
                        adapter = new FeedlyRssAdapter(new List<FeedlyRss>(), Activity, ViewModel.AppConfiguration);
                    }

                    _viewHolder.RecyclerView.SetAdapter(adapter);
                    adapter.NotifyDataSetChanged();
                };
            }

            base.OnCreateOptionsMenu(menu, inflater);
        }
    }
}