using System.Collections.Generic;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Droid.Screens.Base.Adapters;
using JetBrains.Annotations;
using Shared.Configuration.Settings;
using Shared.Database.Rss;

namespace Droid.Screens.RssMessagesList
{
    public class RssMessagesListAdapter : DataBindAdapter<RssMessageServiceModel, IEnumerable<RssMessageServiceModel>, RssMessagesListViewHolder>
    {
        private readonly AppConfiguration _appConfiguration;

        public RssMessagesListAdapter(
            [NotNull] Activity activity,
            AppConfiguration appConfiguration) : base(new RssMessageServiceModel[0], activity)
        {
            _appConfiguration = appConfiguration;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_rss_message, parent, false);
            var holder = new RssMessagesListViewHolder(view, _appConfiguration.LoadAndShowImages);
//
//            holder.ClickView.Click += (sender, args) => { OpenContentActivity(holder); };
//            holder.ClickView.LongClick += (sender, args) => { ItemLongClick(holder, sender); };
//
//            holder.LeftButtonAction += () => { ReadItem(holder); };
//            holder.RightButtonAction += () => { InFavoriteItem(holder); };

            return holder;
        }
    }
}
