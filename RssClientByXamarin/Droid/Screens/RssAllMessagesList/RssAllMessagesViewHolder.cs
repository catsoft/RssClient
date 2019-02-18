﻿using System.ComponentModel;
using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using Droid.Screens.Base;
using Droid.Screens.RssItemMessage;
using FFImageLoading;
using FFImageLoading.Views;
using Shared.Database.Rss;
using Shared.Repository.RssMessage;
using Shared.Services.Locale;

namespace Droid.Screens.RssAllMessagesList
{
    public class RssAllMessagesViewHolder : BaseRssMessageViewHolder, IShowAndLoadImage
    {
        public bool IsShowAndLoadImages { get; }
        
        public TextView Title { get; }
        public TextView Text { get; }
        public TextView CreationDate { get; }
        public TextView Canal { get; }
        public ImageViewAsync ImageView { get; }
        public LinearLayout ClickView { get; }
        public LinearLayout Background { get; }
        public RatingBar RatingBar { get; }
        
        public RssAllMessagesViewHolder(View itemView, bool isShowAndLoadImages) : base(itemView)
        {
            IsShowAndLoadImages = isShowAndLoadImages;
            
            Title = itemView.FindViewById<TextView>(Resource.Id.textView_allMessagesItem_title);
            Text = itemView.FindViewById<TextView>(Resource.Id.textView_allMessagesItem_text);
            CreationDate = itemView.FindViewById<TextView>(Resource.Id.textView_allMessagesItem_date);
            Canal = itemView.FindViewById<TextView>(Resource.Id.textView_allMessagesItem_canal);
            ClickView = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayout_allMessagesItem_content);
            ImageView = itemView.FindViewById<ImageViewAsync>(Resource.Id.imageView_allMessagesItem_image);
            Background = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayout_allMessagesItem_background);
            RatingBar = itemView.FindViewById<RatingBar>(Resource.Id.ratingBar_allMessagesItem_favorite);
            
            ImageView.Visibility = IsShowAndLoadImages.ToVisibility();
        }

        public override void BindData(RssMessageData item)
        {
            Item = item;

            Title.Text = item.Title;
            Text.SetTextAsHtml(item.Text);
            CreationDate.Text = item.CreationDate.ToShortDateLocaleString();
            Canal.Text = item.RssParent.Name;
            RatingBar.Rating = item.IsFavorite ? 1 : 0;
            Background.SetBackgroundColor(item.IsRead ? BackgroundItemSelectColor : BackgroundItemColor);

            if (IsShowAndLoadImages)
            {
                ImageView.Visibility = string.IsNullOrEmpty(item.Url).ToVisibility();
                ImageService.Instance.LoadUrl(item.ImageUrl).Into(ImageView);
            }
        }
    }
}