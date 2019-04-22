using System;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using JetBrains.Annotations;
using Shared.Configuration.Settings;
using Shared.Database.Rss;

namespace Droid.Screens.RssMessagesList
{
    public class RssMessagesListAdapter : BaseRssMessagesAdapter
    {
        private readonly AppConfiguration _appConfiguration;

        public RssMessagesListAdapter(
            [NotNull] Activity activity,
            AppConfiguration appConfiguration) : base(new RssMessageServiceModel[0], activity)
        {
            _appConfiguration = appConfiguration;
        }

        public override event EventHandler<RssMessageServiceModel> Click;
        public override event EventHandler<RssMessageServiceModel> LongClick;
        public override event EventHandler<RssMessageServiceModel> LeftButtonClick;
        public override event EventHandler<RssMessageServiceModel> RightButtonClick;
        
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_rss_message, parent, false);
            var holder = new RssMessagesListViewHolder(view, _appConfiguration.LoadAndShowImages);

            holder.ClickView.Click += (sender, args) => Click?.Invoke(sender, holder.Item);
            holder.ClickView.LongClick += (sender, args) => LongClick?.Invoke(sender, holder.Item);

            holder.LeftButtonAction += () => LeftButtonClick?.Invoke(this, holder.Item);
            holder.RightButtonAction += () => RightButtonClick?.Invoke(this, holder.Item);

            return holder;
        }
    }
}
