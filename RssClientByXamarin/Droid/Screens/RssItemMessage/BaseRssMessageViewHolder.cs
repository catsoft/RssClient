using Android.Graphics;
using Android.Views;
using Droid.Screens.Base.Adapters;
using Droid.Screens.Base.SwipeButtonRecyclerView;
using Shared.Repository.RssMessage;

namespace Droid.Screens.RssItemMessage
{
    public abstract class BaseRssMessageViewHolder : SwipeButtonViewHolder, IDataBind<RssMessageData>
    {
        protected Color BackgroundItemColor = new Color(0, 0, 0, 0);
        protected Color BackgroundItemSelectColor = new Color(0, 0, 0, 95);
        
        public override bool IsLeftButton => true;
        public override bool IsRightButton => true;
        public override string LeftButtonText => "Read";
        public override string RightButtonText => "Favorite";
        
        public RssMessageData Item { get; set; }

        public abstract void BindData(RssMessageData item);
        
        protected BaseRssMessageViewHolder(View itemView) : base(itemView)
        {
        }
    }
}