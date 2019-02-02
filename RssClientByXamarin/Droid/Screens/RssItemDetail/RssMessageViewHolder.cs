using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Droid.Screens.Base.SwipeButtonRecyclerView;
using FFImageLoading.Views;
using Shared.Database.Rss;

namespace Droid.Screens.RssItemDetail
{
    public class RssMessageViewHolder : SwipeButtonViewHolder
    {
        public override bool IsLeftButton => true;
        public override bool IsRightButton => true;
        public override string LeftButtonText => "Read";
        public override string RightButtonText => "Favorite";
        
        public RssMessageViewHolder(View itemView) : base(itemView)
        {
            Title = itemView.FindViewById<TextView>(Resource.Id.textView_messagesItem_title);
            Text = itemView.FindViewById<TextView>(Resource.Id.textView_messagesItem_text);
            CreationDate = itemView.FindViewById<TextView>(Resource.Id.textView_messagesItem_date);
            ClickView = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayout_messagesItem_content);
            ImageView = itemView.FindViewById<ImageViewAsync>(Resource.Id.imageView_messagesItem_image);
            CardView = itemView.FindViewById<CardView>(Resource.Id.cardView_messagesItem_card);
            Background = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayout_messagesItem_background);
        }

        public TextView Title { get; }
        public TextView Text { get; }
        public TextView CreationDate { get; }
        public ImageViewAsync ImageView { get; }
        public LinearLayout ClickView { get; }
        public RssMessageModel Item { get; set; }
        public CardView CardView { get; }
        public LinearLayout Background { get; }
    }
}