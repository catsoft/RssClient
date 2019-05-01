using System;
using System.Reactive.Linq;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Core.Extensions;
using Core.Repositories.Feedly;
using Core.ViewModels.FeedlySearch;
using Droid.Infrastructure.Collections;
using Droid.NativeExtension;
using Droid.NativeExtension.Events;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;

namespace Droid.Screens.FeedlySearch
{
    public class FeedlySearchFragment : BaseFragment<FeedlySearchViewModel>
    {
        [NotNull] private FeedlySearchFragmentViewHolder _viewHolder;

        // ReSharper disable once EmptyConstructor
        // ReSharper disable once NotNullMemberIsNotInitialized
        public FeedlySearchFragment() { }

        protected override int LayoutId => Resource.Layout.fragment_feedly_search;
        public override bool IsRoot => true;

        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState).NotNull();

            HasOptionsMenu = true;

            Title = GetText(Resource.String.feedly_title);

            _viewHolder = new FeedlySearchFragmentViewHolder(view);

            var adapter = new FeedlySearchRssAdapter(Activity, ViewModel.AppConfiguration);
            _viewHolder.RecyclerView.SetAdapter(adapter);

            var adapterUpdater = new AdapterUpdater<FeedlyRssDomainModel>(_viewHolder.RecyclerView, adapter, ViewModel.ListViewModel.SourceList);

            OnActivation(disposable =>
            {
                ViewModel.WhenAnyValue(w => w.IsEmpty)
                    .NotNull()
                    .Subscribe(w => _viewHolder.EmptyTextView.Visibility = w.ToVisibility())
                    .AddTo(disposable);

                adapter.GetClickAddImageEvent()
                    .InvokeCommand(ViewModel.AddFeedlyRssCommand)
                    .AddTo(disposable);

                ViewModel.ListViewModel.ConnectChanges
                    .ObserveOn(RxApp.MainThreadScheduler.NotNull())
                    .Subscribe(w => adapterUpdater.Update(w.NotNull()))
                    .AddTo(disposable);
                
                ViewModel.FindByQueryCommand.IsExecuting
                    .NotNull()
                    .Subscribe(w => _viewHolder.ProgressBar.Visibility = w.ToVisibility())
                    .AddTo(disposable);
            });

            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, [NotNull] MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.menu_feedlySearch, menu);

            if (menu?.FindItem(Resource.Id.menuItem_feedlySearch_search)?.ActionView is SearchView actionView)
                actionView.GetQueryTextChangeEvent()
                    .Subscribe(w => ViewModel.SearchQuery = w.NotNull().NewText)
                    .AddTo(Disposables);

            base.OnCreateOptionsMenu(menu, inflater);
        }
    }
}
