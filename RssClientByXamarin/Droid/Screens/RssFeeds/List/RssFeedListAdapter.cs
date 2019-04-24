using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Core.Configuration.Settings;
using Core.Extensions;
using Core.Services.RssFeeds;
using Droid.NativeExtension.Events;
using Droid.Screens.Base.Adapters;
using Droid.Screens.Base.SwipeRecyclerView;
using JetBrains.Annotations;

namespace Droid.Screens.RssFeeds.List
{
    public class RssFeedListAdapter : DataBindAdapter<RssFeedServiceModel, IEnumerable<RssFeedServiceModel>, RssFeedListItemViewHolder>, 
        IItemTouchHelperAdapter, 
        IClickable<RssFeedServiceModel>, 
        ILongClick<RssFeedServiceModel>
    {
        [NotNull] private readonly AppConfiguration _appConfiguration;

        public RssFeedListAdapter([NotNull] Activity activity, [NotNull] AppConfiguration appConfiguration) : base(new List<RssFeedServiceModel>(), activity)
        {
            _appConfiguration = appConfiguration;
        }

        public void OnItemDismiss(int position)
        {
            var item = Items.ElementAt(position);
            ItemDismiss?.Invoke(this, item);
        }

        public event EventHandler<RssFeedServiceModel> Click;
        
        public event EventHandler<RssFeedServiceModel> LongClick;
        
        public event EventHandler<RssFeedServiceModel> ItemDismiss;

        public override RecyclerView.ViewHolder OnCreateViewHolder([NotNull] ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).NotNull().Inflate(Resource.Layout.list_item_rss, parent, false).NotNull();
            var holder = new RssFeedListItemViewHolder(view, _appConfiguration.LoadAndShowImages);
            holder.ClickView.Click += (sender, args) => Click?.Invoke(this, holder.Item);
            holder.ClickView.LongClick += (sender, args) => LongClick?.Invoke(sender, holder.Item);

            return holder;
        }
    }
}
