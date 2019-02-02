using Android.Graphics;
using Android.Views;
using Droid.Screens.Base.Adapters;
using Droid.Screens.Base.SwipeButtonRecyclerView;
using Shared.Database.Rss;

namespace Droid.Screens.RssItemMessage
{
    public abstract class BaseRssMessageViewHolder : SwipeButtonViewHolder, IDataBind<RssMessageModel>
    {
        protected Color BackgroundItemColor = new Color(0, 0, 0, 0);
        protected Color BackgroundItemSelectColor = new Color(0, 0, 0, 95);
        
        public override bool IsLeftButton => true;
        public override bool IsRightButton => true;
        public override string LeftButtonText => "Read";
        public override string RightButtonText => "Favorite";
        
        public RssMessageModel Item { get; set; }

        public abstract void BindData(RssMessageModel item);
        
        protected BaseRssMessageViewHolder(View itemView) : base(itemView)
        {
        }
    }
}