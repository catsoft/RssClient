using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Droid.Screens.Base.Adapters;
using Droid.Screens.Base.SwipeRecyclerView;
using JetBrains.Annotations;
using Shared.Configuration.Settings;
using Shared.Extensions;
using Shared.Infrastructure.Locale;
using Shared.Services.Rss;

namespace Droid.Screens.RssList
{
    public class RssListAdapter : DataBindAdapter<RssServiceModel, IEnumerable<RssServiceModel>, RssListViewHolder>, IItemTouchHelperAdapter
    {
        [NotNull] private readonly AppConfiguration _appConfiguration;

        public RssListAdapter([NotNull] Activity activity, [NotNull] AppConfiguration appConfiguration) : base(new List<RssServiceModel>(), activity)
        {
            _appConfiguration = appConfiguration;
        }

        public void OnItemDismiss(int position)
        {
            var item = Items.ElementAt(position);
            ItemDismiss?.Invoke(this, item);
        }

        public event EventHandler<RssServiceModel> Click;
        public event EventHandler<RssServiceModel> LongClick;
        public event EventHandler<RssServiceModel> ItemDismiss;

        protected override void BindData(RssListViewHolder holder, RssServiceModel item)
        {
            base.BindData(holder, item);

            holder.SubtitleTextView.Text = item.UpdateTime == null
                ? Activity.GetText(Resource.String.rssList_notUpdated)
                : $"{Activity.GetText(Resource.String.rssList_updated)} {item.UpdateTime.Value.ToShortGeneralLocaleString()}";
            // TODO сделать
//            holder.CountTextView.Text = _rssMessagesRepository.GetCountNewMessagesForModel(item.Id).ToString();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder([NotNull] ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).NotNull().Inflate(Resource.Layout.list_item_rss, parent, false);
            var holder = new RssListViewHolder(view, _appConfiguration.LoadAndShowImages);
            holder.ClickView.Click += (sender, args) => Click?.Invoke(this, holder.Item);
            holder.ClickView.LongClick += (sender, args) => LongClick?.Invoke(sender, holder.Item);

            return holder;
        }
    }
}
