using System;
using System.Collections.Generic;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Droid.Screens.Base.Adapters;
using JetBrains.Annotations;
using Shared.Configuration.Settings;
using Shared.Database.Rss;
using Shared.Extensions;

namespace Droid.Screens.RssAllMessages
{
    public class RssAllMessagesListAdapter : DataBindAdapter<RssMessageServiceModel, IEnumerable<RssMessageServiceModel>, RssAllMessagesViewHolder>
    {
        [NotNull] private readonly AppConfiguration _appConfiguration;

        public RssAllMessagesListAdapter(
            [NotNull] Activity activity,
            [NotNull] AppConfiguration appConfiguration) : base(new List<RssMessageServiceModel>(), activity)
        {
            _appConfiguration = appConfiguration;
        }

        public event EventHandler<RssMessageServiceModel> Click;
        public event EventHandler<RssMessageServiceModel> LongClick;
        public event EventHandler<RssMessageServiceModel> LeftButtonClick;
        public event EventHandler<RssMessageServiceModel> RightButtonClick;

        public override RecyclerView.ViewHolder OnCreateViewHolder([NotNull] ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).NotNull().Inflate(Resource.Layout.list_item_all_rss_message, parent, false).NotNull();
            var holder = new RssAllMessagesViewHolder(view, _appConfiguration.LoadAndShowImages);

            holder.ClickView.Click += (sender, args) => Click?.Invoke(sender, holder.Item);
            holder.ClickView.LongClick += (sender, args) => LongClick?.Invoke(sender, holder.Item);

            holder.LeftButtonAction += () => LeftButtonClick?.Invoke(this, holder.Item);
            holder.RightButtonAction += () => RightButtonClick?.Invoke(this, holder.Item);

            return holder;
        }
    }
}
