using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Droid.NativeExtension;
using Droid.Screens.Navigation;
using ReactiveUI;
using Shared.Extensions;
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
            
            OnActivation(disposable =>
            {
                ViewModel.WhenAnyValue(w => w.FeedlyRss)
                    .Subscribe(UpdateFeeds)
                    .AddTo(disposable);
            });
            
            return view;
        }
        
        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.menu_feedlySearch, menu);

            if (menu?.FindItem(Resource.Id.menuItem_feedlySearch_search)?.ActionView is SearchView actionView)
            {
                actionView.GetQueryTextChangeEvent()
                    .Throttle(TimeSpan.FromSeconds(0.35f))
                    .Select(w => w.NewText ?? "")
                    .InvokeCommand(ViewModel.FindByQueryCommand)
                    .AddTo(Disposables);
            }

            base.OnCreateOptionsMenu(menu, inflater);
        }

        private void UpdateFeeds(IEnumerable<FeedlyRss> feeds)
        {
            var adapter = new FeedlyRssAdapter(feeds?.ToList() ?? new List<FeedlyRss>(), Activity, ViewModel.AppConfiguration);
            _viewHolder.RecyclerView.SetAdapter(adapter);
            adapter.NotifyDataSetChanged();
        }
    }
}