using System.Collections.Generic;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using Droid.Screens.Base;
using Droid.Screens.Base.Adapters;
using FFImageLoading.Views;
using Java.Util.Zip;
using Shared.Api;
using Shared.Configuration.Settings;

namespace Droid.Screens.FeedlySearch
{
    public class FeedlyRssAdapter : DataBindAdapter<FeedlyRss, List<FeedlyRss>, FeedlyRssViewHolder>
    {
        private readonly AppConfiguration _appConfiguration;
        
        public FeedlyRssAdapter(List<FeedlyRss> items, Activity activity, AppConfiguration appConfiguration) : base(items, activity)
        {
            _appConfiguration = appConfiguration;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_feedly_rss, parent, false);
            
            var viewHolder = new FeedlyRssViewHolder(view, _appConfiguration.LoadAndShowImages);

            return viewHolder;
        }
    }

    public class FeedlyRssViewHolder : RecyclerView.ViewHolder, IDataBind<FeedlyRss>, IShowAndLoadImage
    {
        public FeedlyRss Item { get; set; }
        public bool IsShowAndLoadImages { get; }
        
        public ImageButton AddImageView { get; set; }
        public TextView TitleView { get; set; }
        public ImageViewAsync RssIcon { get; set; }
        
        public FeedlyRssViewHolder(View itemView, bool isShowAndLoadImages) : base(itemView)
        {
            IsShowAndLoadImages = isShowAndLoadImages;
            
            RssIcon = itemView.FindViewById<ImageViewAsync>(Resource.Id.imageView_feedlyRss_rssIcon);
            TitleView = itemView.FindViewById<TextView>(Resource.Id.textView_feedlyRss_title);
            AddImageView = itemView.FindViewById<ImageButton>(Resource.Id.imageButton_feedlyRss_add);

            RssIcon.Visibility = IsShowAndLoadImages.ToVisibility();
        }

        public void BindData(FeedlyRss item)
        {
            Item = item;
            
            TitleView.Text = item.Title;

            if (IsShowAndLoadImages)
            {
//                // TODO вынести крутую генерацию фавикона в другое место
//                var uri = new Uri(item.Rss);
//                var favicon = $"{uri.Scheme}://{uri.Host}/favicon.ico";
//
//                // TODO плейсхолдер должен зависить от темы
//                ImageService.Instance.LoadUrl(favicon)
//                    .LoadingPlaceholder("no_image.png", ImageSource.CompiledResource)
//                    .ErrorPlaceholder("no_image.png", ImageSource.CompiledResource)
//                    .Into(RssIcon);   
            }
        }
    }
}