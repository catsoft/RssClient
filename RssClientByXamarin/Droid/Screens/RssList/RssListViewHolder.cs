using System.Globalization;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Autofac;
using Droid.NativeExtension;
using Droid.Screens.Base;
using Droid.Screens.Base.Adapters;
using FFImageLoading;
using FFImageLoading.Views;
using FFImageLoading.Work;
using Java.Security;
using Shared;
using Shared.Database.Rss;
using Shared.Services.Locale;

namespace Droid.Screens.RssList
{
    public class RssListViewHolder : RecyclerView.ViewHolder, IDataBind<RssModel>, IShowAndLoadImage
    {
        public bool IsShowAndLoadImages { get; }
        
        public RssModel Item { get; set; }
        
        public TextView TitleTextView { get; }
        public TextView SubtitleTextView { get; }
        public ImageViewAsync IconView { get; }
        public TextView CountTextView { get; }
        public LinearLayout ClickView { get; }
        
        public RssListViewHolder(View itemView, bool showImages) : base(itemView)
        {
            IsShowAndLoadImages = showImages;
            
            TitleTextView = itemView.FindViewById<TextView>(Resource.Id.textView_listItemRss_title);
            SubtitleTextView = itemView.FindViewById<TextView>(Resource.Id.textView_listItemRss_subtitle);
            ClickView = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayout_listItemRss_content);
            IconView = itemView.FindViewById<ImageViewAsync>(Resource.Id.imageView_listItemRss_rssIcon);
            CountTextView = itemView.FindViewById<TextView>(Resource.Id.textView_listItemRss_rssCount);

            IconView.Visibility = IsShowAndLoadImages.ToVisibility();
        }
        
        public void BindData(RssModel item)
        {
            TitleTextView.Text = item.Name;
            Item = item;

            if (IsShowAndLoadImages)
            {
                ImageService.Instance.LoadUrl(item.UrlPreviewImage)
                    .LoadingPlaceholder("no_image.png", ImageSource.CompiledResource)
                    .ErrorPlaceholder("no_image.png", ImageSource.CompiledResource)
                    .Into(IconView);    
            }
        }
    }
}