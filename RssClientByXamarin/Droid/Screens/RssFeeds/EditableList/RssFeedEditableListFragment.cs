using System;
using Android.OS;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Core.Extensions;
using Core.Services.RssFeeds;
using Core.ViewModels.RssFeeds.EditableList;
using Droid.Infrastructure.Collections;
using Droid.NativeExtension;
using Droid.NativeExtension.Events;
using Droid.Screens.Base.DragRecyclerView;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;

namespace Droid.Screens.RssFeeds.EditableList
{
    public class RssFeedEditableListFragment : BaseFragment<RssFeedEditableListViewModel>
    {
        // ReSharper disable once NotNullMemberIsNotInitialized
        [NotNull] private RssFeedEditableListFragmentViewHolder _viewHolder;

        protected override int LayoutId => Resource.Layout.fragment_rss_edit_list;
        public override bool IsRoot => false;

        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState).NotNull();

            Title = GetText(Resource.String.rssEditList_title);

            _viewHolder = new RssFeedEditableListFragmentViewHolder(view);

            var adapter = new RssFeedEditableListAdapter(Activity);
            _viewHolder.RecyclerView.SetAdapter(adapter);

            var callBack = new ReorderHelperCallback(adapter);
            var helper = new ItemTouchHelper(callBack);
            helper.AttachToRecyclerView(_viewHolder.RecyclerView);
            adapter.OnStartDrag += holder => helper.StartDrag(holder);

            var adapterUpdater = new AdapterUpdater<RssFeedServiceModel>(_viewHolder.RecyclerView, adapter, ViewModel.ListViewModel.SourceList);

            OnActivation(disposable =>
            {
                this.BindCommand(ViewModel,
                        model => model.OpenCreateItemScreenCommand,
                        fragment => fragment._viewHolder.FloatingActionButton)
                    .AddTo(disposable);

                ViewModel.WhenAnyValue(model => model.ListViewModel.SourceList)
                    .NotNull()
                    .Subscribe(w => adapter.Items = w.Items)
                    .AddTo(disposable);

                ViewModel.ListViewModel.ConnectChanges
                    .NotNull()
                    .Subscribe(w => adapterUpdater.Update(w))
                    .AddTo(disposable);

                adapter.GetItemDeleteEvent()
                    .InvokeCommand(ViewModel.DeleteItemCommand)
                    .AddTo(disposable);

                adapter.GetItemMoveEvent()
                    .InvokeCommand(ViewModel.MoveItemCommand)
                    .AddTo(disposable);

                ViewModel.WhenAnyValue(model => model.ListViewModel.IsEmpty)
                    .Subscribe(w => _viewHolder.EmptyEditText.Visibility = w.ToVisibility())
                    .AddTo(disposable);

                ViewModel.LoadCommand.Execute().Subscribe();
            });

            return view;
        }
    }
}
