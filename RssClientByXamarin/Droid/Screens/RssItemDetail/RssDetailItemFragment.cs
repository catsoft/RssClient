using System;
using System.ComponentModel;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.Container;
using Droid.Screens.Navigation;
using Droid.Screens.RssEdit;
using FFImageLoading.Views;
using RssClient.Repository;
using Shared;
using Shared.Database.Rss;
using Shared.Repository;
using Shared.Services.Navigator;
using Shared.ViewModels;
using Xamarin.Essentials;

namespace Droid.Screens.RssItemDetail
{
    public class RssDetailItemFragment : TitleFragment
    {
        private string _itemId;
        private RssModel Item => _rssRepository.Find(_itemId);

        [Inject]
        private IRssMessagesRepository _rssMessagesRepository;
        
        [Inject]
        private IRssRepository _rssRepository;
        
        [Inject]
        private INavigator _navigator;

        protected override int LayoutId => Resource.Layout.fragment_rss_detail;
        public override bool RootFragment => false;
        
        public RssDetailItemFragment()
        {
            
        }
        
        public RssDetailItemFragment(string itemId)
        {
            _itemId = itemId;
        }
        
        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            
            outState.PutString(nameof(_itemId), _itemId);
        }

        protected override void RestoreState(Bundle saved)
        {
            _itemId = saved.GetString(nameof(_itemId));
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            HasOptionsMenu = true;

            var item = Item;
            Title = item.Name;
            
            var list = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_rssDetail_messageList);
            list.SetLayoutManager(new LinearLayoutManager(Activity, LinearLayoutManager.Vertical, false));

            var refreshLayout = view.FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout_rssDetail_refresher);
            refreshLayout.Refresh += async (sender, args) =>
            {
                await _rssRepository.StartUpdateAllByInternet(item.Rss, item.Id);
                refreshLayout.Refreshing = false;
            };

            var items = _rssMessagesRepository.GetMessagesForRss(item);
            var adapter = new RssMessageAdapter(items.ToList(), Activity);
            list.SetAdapter(adapter);
            adapter.NotifyDataSetChanged();

            item.PropertyChanged += (sender, args) =>
            {
                adapter.Items.Clear();
                var newItems = _rssMessagesRepository.GetMessagesForRss(item);
                adapter.Items.AddRange(newItems);
                adapter.NotifyDataSetChanged();
            };

            _rssRepository.StartUpdateAllByInternet(item.Rss, item.Id);

            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.menu_rssDetail, menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.menuItem_rssDetail_remove)
            {
                DeleteItem();
            }
            else if (item.ItemId == Resource.Id.menuItem_rssDetail_edit)
            {
                EditItem();
            }
            else if (item.ItemId == Resource.Id.menuItem_rssDetail_share)
            {
                ShareItem();
            }

            return base.OnOptionsItemSelected(item);
        }

        private async void ShareItem()
        {
            await Share.RequestAsync(Item.Rss);
        }

        private void EditItem()
        {
            var navigator = App.Container.Resolve<INavigator>();
            var editWay = App.Container.Resolve<IWay<RssEditViewModel, RssEditViewModel.Way.WayData>>();
            editWay.Data = new RssEditViewModel.Way.WayData(_itemId);
            navigator.Go(editWay);
        }

        private void DeleteItem()
        {
            var builder = new AlertDialog.Builder(Activity);
            builder.SetPositiveButton(GetText(Resource.String.rssDeleteDialog_positiveTitle), (sender, args) =>
            {
                _rssRepository.Remove(Item);
                _navigator.GoBack();
            });
            builder.SetNegativeButton(GetText(Resource.String.rssDeleteDialog_negativeTitle), (sender, args) => { });
            builder.SetTitle(GetText(Resource.String.rssDeleteDialog_Title));
            builder.Show();
        }
    }
}