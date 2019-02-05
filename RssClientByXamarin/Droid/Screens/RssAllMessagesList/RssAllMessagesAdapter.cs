using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Android.App;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Autofac;
using Droid.NativeExtension;
using Droid.Screens.Base.SwipeButtonRecyclerView;
using Droid.Screens.RssItemMessage;
using FFImageLoading;
using Shared;
using Shared.Database.Rss;
using Shared.Repository;
using Shared.Services.Locale;
using Shared.Services.Navigator;
using Shared.ViewModels;

namespace Droid.Screens.RssAllMessagesList
{
    public class RssAllMessagesListAdapter : BaseRssMessageAdapter<IEnumerable<RssMessageModel>, RssAllMessagesViewHolder>
    {
        public RssAllMessagesListAdapter(IQueryable<RssMessageModel> items, Activity activity, IRssMessagesRepository rssMessagesRepository) : base(items.ToList(), activity, rssMessagesRepository)
        {
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_all_rss_message, parent, false);
            var holder = new RssAllMessagesViewHolder(view);

            holder.ClickView.Click += (sender, args) => { OpenContentActivity(holder.Item); };
            holder.ClickView.LongClick += (sender, args) => { ItemLongClick(holder.Item, sender); };

            holder.LeftButtonAction += () => { ReadItem(holder.Item); };
            holder.RightButtonAction += () => { InFavoriteItem(holder.Item); };
            
            return holder;
        }

        public override void OnViewDetachedFromWindow(Java.Lang.Object holder)
        {
            if (holder is RssAllMessagesViewHolder rssAllMessagesViewHolder && rssAllMessagesViewHolder.Item != null)
            {
                rssAllMessagesViewHolder.Item.PropertyChanged -= rssAllMessagesViewHolder.UpdateHimself;
            }
            
            base.OnViewDetachedFromWindow(holder);
        }
    }
}