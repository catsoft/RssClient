using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Droid.Screens.RssItemMessage;
using Shared.Configuration;
using Shared.Database.Rss;
using Shared.Repository;

namespace Droid.Screens.RssAllMessagesList
{
    public class RssAllMessagesListAdapter : BaseRssMessageAdapter<IEnumerable<RssMessageModel>, RssAllMessagesViewHolder>
    {
        private readonly AppConfiguration _appConfiguration;
        
        public RssAllMessagesListAdapter(IQueryable<RssMessageModel> items, Activity activity, IRssMessagesRepository rssMessagesRepository, AppConfiguration appConfiguration) : base(items.ToList(), activity, rssMessagesRepository)
        {
            _appConfiguration = appConfiguration;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_all_rss_message, parent, false);
            var holder = new RssAllMessagesViewHolder(view, _appConfiguration.LoadAndShowImages);

            holder.ClickView.Click += (sender, args) => { OpenContentActivity(holder.Item); };
            holder.ClickView.LongClick += (sender, args) => { ItemLongClick(holder.Item, sender); };

            holder.LeftButtonAction += () => { ReadItem(holder.Item); };
            holder.RightButtonAction += () => { InFavoriteItem(holder.Item); };
            
            return holder;
        }
    }
}