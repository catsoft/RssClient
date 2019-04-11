using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Autofac;
using Droid.Container;
using Droid.Screens.Base.DragRecyclerView;
using Droid.Screens.Navigation;
using Shared;
using Shared.Infrastructure.Navigation;
using Shared.Repository.Rss;
using Shared.ViewModels.RssCreate;

namespace Droid.Screens.RssEditList
{
    public class RssEditListFragment : TitleFragment
    {
        [Inject]
        private INavigator _navigator;

        [Inject]
        private IRssRepository _rssRepository;
        
        protected override int LayoutId => Resource.Layout.fragment_rss_edit_list;
        public override bool RootFragment => false;
        
        public RssEditListFragment()
        {
            
        }
        
        protected override void RestoreState(Bundle saved)
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            
            Title = GetText(Resource.String.rssEditList_title);
            
            var recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_rssEditList_list);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Context, LinearLayoutManager.Vertical, false));
            recyclerView.AddItemDecoration(new DividerItemDecoration(Context, DividerItemDecoration.Vertical));
            recyclerView.SaveEnabled = true;

            var fab = view.FindViewById<FloatingActionButton>(Resource.Id.fab_rssEditList_addRss);

            fab.Click += (sender, args) => { _navigator.Go(App.Container.Resolve<RssCreateViewModel.Way>()); };

            var items = _rssRepository.GetList();
            var adapter = new RssListEditAdapter(items, Activity, _rssRepository);
            recyclerView.SetAdapter(adapter);

            var callBack = new ReorderHelperCallback(adapter);
            var helper = new ItemTouchHelper(callBack);
            helper.AttachToRecyclerView(recyclerView);

            adapter.OnStartDrag += holder => { helper.StartDrag(holder); };
            
            return view;
        }
    }
}