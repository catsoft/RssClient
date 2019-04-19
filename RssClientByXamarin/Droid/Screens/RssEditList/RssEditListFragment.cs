#region

using System;
using Android.OS;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Droid.Infrastructure.Collections;
using Droid.NativeExtension;
using Droid.Screens.Base.DragRecyclerView;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;
using Shared.Extensions;
using Shared.Services.Rss;
using Shared.ViewModels.RssListEdit;

#endregion

namespace Droid.Screens.RssEditList
{
    public class RssEditListFragment : BaseFragment<RssListEditViewModel>
    {
        // ReSharper disable once NotNullMemberIsNotInitialized
        [NotNull] private RssEditListFragmentViewHolder _viewHolder;

        protected override int LayoutId => Resource.Layout.fragment_rss_edit_list;
        public override bool IsRoot => false;

        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState).NotNull();

            Title = GetText(Resource.String.rssEditList_title);

            _viewHolder = new RssEditListFragmentViewHolder(view);

            var adapter = new RssListEditAdapter(Activity);
            _viewHolder.RecyclerView.SetAdapter(adapter);

            var callBack = new ReorderHelperCallback(adapter);
            var helper = new ItemTouchHelper(callBack);
            helper.AttachToRecyclerView(_viewHolder.RecyclerView);
            adapter.OnStartDrag += holder => helper.StartDrag(holder);

            var adapterUpdater = new AdapterUpdater<RssServiceModel>(adapter, ViewModel.SourceList);

            OnActivation(disposable =>
            {
                this.BindCommand(ViewModel,
                        model => model.OpenCreateItemScreenCommand,
                        fragment => fragment._viewHolder.FloatingActionButton)
                    .AddTo(disposable);

                ViewModel.WhenAnyValue(model => model.SourceList)
                    .NotNull()
                    .Subscribe(w => adapter.Items = w.Items)
                    .AddTo(disposable);

                ViewModel.ConnectChanges()
                    .NotNull()
                    .Subscribe(w => adapterUpdater.Update(w))
                    .AddTo(disposable);

                adapter.GetItemDeleteEvent()
                    .InvokeCommand(ViewModel.DeleteItemCommand)
                    .AddTo(disposable);

                adapter.GetItemMoveEvent()
                    .InvokeCommand(ViewModel.MoveItemCommand)
                    .AddTo(disposable);

                ViewModel.WhenAnyValue(model => model.IsEmpty)
                    .Subscribe(w => _viewHolder.EmptyEditText.Visibility = w.ToVisibility())
                    .AddTo(disposable);

                ViewModel.LoadCommand.Execute().Subscribe();
            });

            return view;
        }
    }
}
