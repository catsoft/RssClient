using System.Collections.Generic;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Shared.Database.Rss;
using Shared.Repository;

namespace Droid.Screens.RssItemMessage
{
    public class RssItemMessageAdapter : BaseRssMessageAdapter<List<RssMessageModel>, RssItemMessageViewHolder>
    {
        public RssItemMessageAdapter(List<RssMessageModel> items, Activity activity, IRssMessagesRepository rssMessagesRepository) : base(items, activity, rssMessagesRepository)
        {
        }
        
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_rss_message, parent, false);
            var holder = new RssItemMessageViewHolder(view);

            holder.ClickView.Click += (sender, args) => { OpenContentActivity(holder.Item); };
            holder.ClickView.LongClick += (sender, args) => { ItemLongClick(holder.Item, sender); };

            holder.LeftButtonAction += () => { ReadItem(holder.Item); };
            holder.RightButtonAction += () => { InFavoriteItem(holder.Item); };
            
            return holder;
        }
    }
}