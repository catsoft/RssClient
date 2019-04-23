using System;
using System.Collections.Generic;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Core.Configuration.Settings;
using Core.Services.RssMessages;
using Droid.NativeExtension.Events;
using Droid.Resources;
using Droid.Screens.Base.Adapters;
using JetBrains.Annotations;

namespace Droid.Screens.Messages.RssFeedMessagesList
{
    public class RssFeedMessagesListAdapter : DataBindAdapter<RssMessageServiceModel, IEnumerable<RssMessageServiceModel>, RssFeedMessageItemListViewHolder>,
        IClickable<RssMessageServiceModel>,
        ILongClick<RssMessageServiceModel>,
        ISwipeActions<RssMessageServiceModel>

    {
        private readonly AppConfiguration _appConfiguration;

        public RssFeedMessagesListAdapter(
            [NotNull] Activity activity,
            AppConfiguration appConfiguration) : base(new RssMessageServiceModel[0], activity)
        {
            _appConfiguration = appConfiguration;
        }

        public event EventHandler<RssMessageServiceModel> Click;
        
        public event EventHandler<RssMessageServiceModel> LongClick;
        
        public event EventHandler<RssMessageServiceModel> LeftSwipeAction;
        
        public event EventHandler<RssMessageServiceModel> RightSwipeAction;

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_rss_message, parent, false);
            var holder = new RssFeedMessageItemListViewHolder(view, _appConfiguration.LoadAndShowImages);

            holder.ClickView.Click += (sender, args) => Click?.Invoke(sender, holder.Item);
            holder.ClickView.LongClick += (sender, args) => LongClick?.Invoke(sender, holder.Item);

            holder.LeftButtonAction += () => LeftSwipeAction?.Invoke(this, holder.Item);
            holder.RightButtonAction += () => RightSwipeAction?.Invoke(this, holder.Item);

            return holder;
        }
    }
}