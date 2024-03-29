using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Core.Extensions;
using Core.Repositories.Feedly;
using Droid.NativeExtension;
using Droid.Screens.Base;
using Droid.Screens.Base.Adapters;
using FFImageLoading;
using FFImageLoading.Views;
using FFImageLoading.Work;
using JetBrains.Annotations;

namespace Droid.Screens.FeedlySearch
{
    public class FeedlyRssViewHolder : RecyclerView.ViewHolder, IDataBind<FeedlyRssDomainModel>, IShowAndLoadImage
    {
        public FeedlyRssViewHolder([NotNull] View itemView, bool isShowAndLoadImages) : base(itemView)
        {
            IsShowAndLoadImages = isShowAndLoadImages;

            RssIcon = itemView.FindNotNull<ImageViewAsync>(Resource.Id.imageView_feedlyRss_rssIcon);
            TitleView = itemView.FindNotNull<TextView>(Resource.Id.textView_feedlyRss_title);
            AddImageView = itemView.FindNotNull<ImageButton>(Resource.Id.imageButton_feedlyRss_add);

            RssIcon.Visibility = IsShowAndLoadImages.ToVisibility();
        }
        
        [NotNull] public ImageButton AddImageView { get; }
        
        [NotNull] public TextView TitleView { get; }
        
        [NotNull] public ImageViewAsync RssIcon { get; }
        
        public FeedlyRssDomainModel Item { get; set; }
        
        public bool IsShowAndLoadImages { get; }

        public void BindData(FeedlyRssDomainModel item)
        {
            Item = item;

            TitleView.Text = item.Title;

            if (IsShowAndLoadImages)
                ImageService.Instance.NotNull()
                    .LoadUrl(item.IconUrl)
                    .NotNull()
                    .LoadingPlaceholder("no_image.png", ImageSource.CompiledResource)
                    .NotNull()
                    .ErrorPlaceholder("no_image.png", ImageSource.CompiledResource)
                    .Into(RssIcon);
        }
    }
}
