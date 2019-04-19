#region

using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using Droid.Screens.Base;
using Droid.Screens.Base.Adapters;
using FFImageLoading.Views;
using JetBrains.Annotations;
using Shared.Extensions;
using Shared.Repositories.Feedly;

#endregion

namespace Droid.Screens.FeedlySearch
{
    public class FeedlyRssViewHolder : RecyclerView.ViewHolder, IDataBind<FeedlyRssDomainModel>, IShowAndLoadImage
    {
        public FeedlyRssViewHolder([NotNull] View itemView, bool isShowAndLoadImages) : base(itemView)
        {
            IsShowAndLoadImages = isShowAndLoadImages;

            RssIcon = itemView.FindViewById<ImageViewAsync>(Resource.Id.imageView_feedlyRss_rssIcon).NotNull();
            TitleView = itemView.FindViewById<TextView>(Resource.Id.textView_feedlyRss_title).NotNull();
            AddImageView = itemView.FindViewById<ImageButton>(Resource.Id.imageButton_feedlyRss_add).NotNull();

            RssIcon.Visibility = IsShowAndLoadImages.ToVisibility();
        }

        [NotNull] public ImageButton AddImageView { get; }
        [NotNull] public TextView TitleView { get; }
        [NotNull] public ImageViewAsync RssIcon { get; }
        public FeedlyRssDomainModel Item { get; set; }

        public void BindData(FeedlyRssDomainModel item)
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

        public bool IsShowAndLoadImages { get; }
    }
}
