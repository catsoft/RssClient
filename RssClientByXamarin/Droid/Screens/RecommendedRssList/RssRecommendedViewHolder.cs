using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using Droid.Screens.Base;
using Droid.Screens.Base.Adapters;
using FFImageLoading;
using FFImageLoading.Views;
using FFImageLoading.Work;
using Shared.Database.Rss;

namespace Droid.Screens.RecommendedRssList
{
    public class RssRecommendedViewHolder : RecyclerView.ViewHolder, IDataBind<RssRecommendationModel>, IShowAndLoadImage
    {
        public bool IsShowAndLoadImages { get; }
        
        public RssRecommendationModel Item { get; set; }
        
        public ImageViewAsync RssIcon { get; }
        public TextView TitleView { get; }
        public ImageButton AddImageView { get; }
        
        public RssRecommendedViewHolder(View itemView, bool isShowAndLoadImages) : base(itemView)
        {
            IsShowAndLoadImages = isShowAndLoadImages;
            
            RssIcon = itemView.FindViewById<ImageViewAsync>(Resource.Id.imageView_rssRecommendedItem_rssIcon);
            TitleView = itemView.FindViewById<TextView>(Resource.Id.textView_rssRecommendedItem_title);
            AddImageView = itemView.FindViewById<ImageButton>(Resource.Id.imageButton_rssRecommendedItem_add);

            RssIcon.Visibility = IsShowAndLoadImages.ToVisibility();
        }

        public void BindData(RssRecommendationModel item)
        {
            Item = item;
            
            TitleView.Text = item.Rss;

            if (IsShowAndLoadImages)
            {
                // TODO вынести крутую генерацию фавикона в другое место
                var uri = new Uri(item.Rss);
                var favicon = $"{uri.Scheme}://{uri.Host}/favicon.ico";

                // TODO плейсхолдер должен зависить от темы
                ImageService.Instance.LoadUrl(favicon)
                    .LoadingPlaceholder("no_image.png", ImageSource.CompiledResource)
                    .ErrorPlaceholder("no_image.png", ImageSource.CompiledResource)
                    .Into(RssIcon);   
            }
        }
    }
}