using Android.Graphics;
using Android.Views;
using Core.Services.RssMessages;
using Droid.Screens.Base.Adapters;
using Droid.Screens.Base.SwipeButtonRecyclerView;

namespace Droid.Screens.Messages
{
    public abstract class BaseMessageItemViewHolder : SwipeButtonViewHolder, IDataBind<RssMessageServiceModel>
    {
        protected Color BackgroundItemColor = new Color(0, 0, 0, 0);
        protected Color BackgroundItemSelectColor = new Color(0, 0, 0, 95);

        protected BaseMessageItemViewHolder(View itemView) : base(itemView) { }

        public override bool IsLeftButton => true;
        public override bool IsRightButton => true;
        public override string LeftButtonText => "Read";
        public override string RightButtonText => "Favorite";

        public RssMessageServiceModel Item { get; set; }

        public abstract void BindData(RssMessageServiceModel item);
    }
}
