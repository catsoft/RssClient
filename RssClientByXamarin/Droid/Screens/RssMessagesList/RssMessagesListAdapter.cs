using System.Collections.Generic;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Shared.Configuration.Settings;
using Shared.Repository.RssMessage;

namespace Droid.Screens.RssMessagesList
{
    public class RssMessagesListAdapter : BaseRssMessagesAdapter<RssMessagesListViewHolder>
    {
        private readonly AppConfiguration _appConfiguration;
        
        public RssMessagesListAdapter(List<RssMessageData> items, Activity activity, IRssMessagesRepository rssMessagesRepository, AppConfiguration appConfiguration) : base(items, activity, rssMessagesRepository)
        {
            _appConfiguration = appConfiguration;
        }
        
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_rss_message, parent, false);
            var holder = new RssMessagesListViewHolder(view, _appConfiguration.LoadAndShowImages);

            holder.ClickView.Click += (sender, args) => { OpenContentActivity(holder); };
            holder.ClickView.LongClick += (sender, args) => { ItemLongClick(holder, sender); };

            holder.LeftButtonAction += () => { ReadItem(holder); };
            holder.RightButtonAction += () => { InFavoriteItem(holder); };
             
            return holder;
        }
    }
}