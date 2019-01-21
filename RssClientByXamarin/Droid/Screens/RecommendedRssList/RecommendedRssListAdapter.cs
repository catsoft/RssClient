using System;
using System.Collections.Generic;
using System.Linq;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using FFImageLoading;
using FFImageLoading.Work;
using Realms;
using Shared.Database.Rss;
using Xamarin.Essentials;

namespace Droid.Screens.RecommendedRssList
{
    public class RecommendedRssListAdapter : RecyclerView.Adapter
    {
        private const int HeaderViewType = 0;
        private const int ItemViewType = 1;
        
        private readonly int _itemsCount;
        private readonly object[] _items;
        
        public RecommendedRssListAdapter(IQueryable<RssRecommendationModel> recommendationModels)
        {
            var items = recommendationModels.ToList();
            var grouped = items.GroupBy(w => w.Category).ToDictionary(w => w.Key, w => w.ToArray());
            _itemsCount = items.Count + grouped.Count;

            var itemsAdapter = new List<object>();
            foreach (var (key, value) in grouped)
            {
                itemsAdapter.Add(key);
                itemsAdapter.AddRange(value);
            }

            _items = itemsAdapter.ToArray();
        }

        public override int GetItemViewType(int position)
        {
            var item = _items[position];
            return item is string ? HeaderViewType : ItemViewType;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is RssRecommendedHeaderViewHolder headerViewHolder)
            {
                var item = _items[position] as string;
                headerViewHolder.TitleView.Text = item;
            }
            else if (holder is RssRecommendedItemViewHolder itemViewHolder)
            {
                var item = _items[position] as RssRecommendationModel;
                itemViewHolder.TitleView.Text = item.Rss;
                
                var uri = new Uri(item.Rss);
                var c = uri.GetLeftPart(UriPartial.Scheme);
                var b = uri.GetLeftPart(UriPartial.Authority);

                var favicon = $"{uri.Scheme}://{uri.Host}/favicon.ico";
                
                ImageService.Instance.LoadUrl(favicon)
                    .LoadingPlaceholder("no_image.png", ImageSource.CompiledResource)
                    .ErrorPlaceholder("no_image.png", ImageSource.CompiledResource)
                    .Into(itemViewHolder.RssIcon);
            }
        }
        
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var inflater = LayoutInflater.From(parent.Context);
            
            if (viewType == HeaderViewType)
            {
                var view = inflater.Inflate(Resource.Layout.list_header_recommended_rss, parent, false);
                return new RssRecommendedHeaderViewHolder(view);
            }
            else
            {
                var view = inflater.Inflate(Resource.Layout.list_item_recommended_rss, parent, false);
                return new RssRecommendedItemViewHolder(view);
            }
        }

        public override int ItemCount => _itemsCount;
    }
}