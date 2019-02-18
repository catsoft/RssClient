using System.Collections.Generic;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Shared.Configuration.Settings;
using Shared.Database.Rss;
using Shared.Repository.RssMessage;

namespace Droid.Screens.RssItemMessage
{
    public class RssItemMessageAdapter : BaseRssMessageAdapter<List<RssMessageData>, RssItemMessageViewHolder>
    {
        private readonly AppConfiguration _appConfiguration;
        
        public RssItemMessageAdapter(List<RssMessageData> items, Activity activity, IRssMessagesRepository rssMessagesRepository, AppConfiguration appConfiguration) : base(items, activity, rssMessagesRepository)
        {
            _appConfiguration = appConfiguration;
        }
        
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_rss_message, parent, false);
            var holder = new RssItemMessageViewHolder(view, _appConfiguration.LoadAndShowImages);

            holder.ClickView.Click += (sender, args) => { OpenContentActivity(holder.Item); };
            holder.ClickView.LongClick += (sender, args) => { ItemLongClick(holder.Item, sender); };

            holder.LeftButtonAction += () => { ReadItem(holder.Item); };
            holder.RightButtonAction += () => { InFavoriteItem(holder.Item); };
             
            return holder;
        }
    }
}