using System;
using System.Collections.Generic;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Droid.Screens.Base.Adapters;
using JetBrains.Annotations;
using Shared.Configuration.Settings;
using Shared.Database.Rss;
using Shared.ViewModels.RssMessage;

namespace Droid.Screens.RssMessagesList
{
    public class RssMessagesListAdapter : DataBindAdapter<RssMessageServiceModel, IEnumerable<RssMessageServiceModel>, RssMessagesListViewHolder>
    {
        private readonly AppConfiguration _appConfiguration;

        public RssMessagesListAdapter(
            [NotNull] Activity activity,
            AppConfiguration appConfiguration) : base(new RssMessageServiceModel[0], activity)
        {
            _appConfiguration = appConfiguration;
        }

        public virtual event EventHandler<RssMessageServiceModel> Click;
        public virtual event EventHandler<RssMessageServiceModel> LongClick;
        public virtual event EventHandler<RssMessageServiceModel> LeftButtonClick;
        public virtual event EventHandler<RssMessageServiceModel> RightButtonClick;
        
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
