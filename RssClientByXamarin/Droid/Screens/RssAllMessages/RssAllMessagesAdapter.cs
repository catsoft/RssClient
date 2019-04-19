#region

using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Droid.Screens.RssMessagesList;
using JetBrains.Annotations;
using Shared.Configuration.Settings;
using Shared.Extensions;
using Shared.Repositories.RssMessage;

#endregion

namespace Droid.Screens.RssAllMessages
{
    public class RssAllMessagesListAdapter : BaseRssMessagesAdapter<RssAllMessagesViewHolder>
    {
        [NotNull] private readonly AppConfiguration _appConfiguration;

        public RssAllMessagesListAdapter(
            IEnumerable<RssMessageDomainModel> items,
            Activity activity,
            IRssMessagesRepository rssMessagesRepository,
            [NotNull] AppConfiguration appConfiguration) : base(items.ToList(), activity, rssMessagesRepository)
        {
            _appConfiguration = appConfiguration;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder([NotNull] ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).NotNull().Inflate(Resource.Layout.list_item_all_rss_message, parent, false);
            var holder = new RssAllMessagesViewHolder(view, _appConfiguration.LoadAndShowImages);

            holder.ClickView.Click += (sender, args) => { OpenContentActivity(holder); };
            holder.ClickView.LongClick += (sender, args) => { ItemLongClick(holder, sender); };

            holder.LeftButtonAction += () => { ReadItem(holder); };
            holder.RightButtonAction += () => { InFavoriteItem(holder); };

            return holder;
        }
    }
}
