using System;
using System.Collections.Generic;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Core.Configuration.Settings;
using Core.Extensions;
using Core.Services.RssMessages;
using Droid.NativeExtension.Events;
using Droid.Screens.Base.Adapters;
using JetBrains.Annotations;

namespace Droid.Screens.Messages.AllMessages
{
    public abstract class BaseAllMessagesListAdapter : DataBindAdapter<RssMessageServiceModel, IEnumerable<RssMessageServiceModel>, AllMessageListItemViewHolder>,
        IClickable<RssMessageServiceModel>, 
        ILongClick<RssMessageServiceModel>, 
        ISwipeActions<RssMessageServiceModel>
    {
        [NotNull] private readonly AppConfiguration _appConfiguration;

        protected BaseAllMessagesListAdapter(
            [NotNull] Activity activity,
            [NotNull] AppConfiguration appConfiguration) : base(new List<RssMessageServiceModel>(), activity)
        {
            _appConfiguration = appConfiguration;
        }

        public event EventHandler<RssMessageServiceModel> Click;
        
        public event EventHandler<RssMessageServiceModel> LongClick;
        
        public event EventHandler<RssMessageServiceModel> LeftSwipeAction;
        
        public event EventHandler<RssMessageServiceModel> RightSwipeAction;

        public override RecyclerView.ViewHolder OnCreateViewHolder([NotNull] ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).NotNull().Inflate(Resource.Layout.list_item_all_rss_message, parent, false).NotNull();
            var holder = new AllMessageListItemViewHolder(view, _appConfiguration.LoadAndShowImages);

            holder.RootRelativeLayout.Click += (sender, args) => Click?.Invoke(sender, holder.Item);
            holder.RootRelativeLayout.LongClick += (sender, args) => LongClick?.Invoke(sender, holder.Item);

            holder.LeftButtonAction += () => LeftSwipeAction?.Invoke(this, holder.Item);
            holder.RightButtonAction += () => RightSwipeAction?.Invoke(this, holder.Item);

            return holder;
        }
    }
}