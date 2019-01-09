﻿using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Database.Rss;

namespace RssClient.App.Rss.Detail
{
	public class RssMessageViewHolder : RecyclerView.ViewHolder
    {
        public RssMessageViewHolder(View itemView) : base(itemView)
        {
            Title = itemView.FindViewById<TextView>(Resource.Id.textView_messagesItem_title);
            Text = itemView.FindViewById<TextView>(Resource.Id.textView_messagesItem_text);
            CreationDate = itemView.FindViewById<TextView>(Resource.Id.textView_messagesItem_date);
            ClickView = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayout_messagesItem_content);
            ImageView = itemView.FindViewById<ImageView>(Resource.Id.imageView_messagesItem_image);
        }

        public TextView Title { get; }
        public TextView Text { get; }
        public TextView CreationDate { get; }
        public ImageView ImageView { get; set; }
        public RssMessageModel Item { get; set; }
        public LinearLayout ClickView { get; set; }
    }
}