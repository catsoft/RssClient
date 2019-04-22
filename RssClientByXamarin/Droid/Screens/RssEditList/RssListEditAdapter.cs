using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Droid.Screens.Base.Adapters;
using Droid.Screens.Base.DragRecyclerView;
using JetBrains.Annotations;
using Shared.Extensions;
using Shared.Infrastructure.Locale;
using Shared.Services.Rss;
using Shared.ViewModels.RssListEdit;

namespace Droid.Screens.RssEditList
{
    public class RssListEditAdapter : DataBindAdapter<RssServiceModel, IEnumerable<RssServiceModel>, RssListEditViewHolder>, IReorderListHelperAdapter
    {
        public RssListEditAdapter([NotNull] Activity activity) : base(new List<RssServiceModel>(), activity) { }

        public void OnMove(int fromPosition, int toPosition)
        {
            var args = new MoveEventArgs(fromPosition, toPosition);
            OnMoveEvent?.Invoke(this, args);
        }

        public event EventHandler<RssServiceModel> DeleteClick;
        public event EventHandler<MoveEventArgs> OnMoveEvent;
        public event Action<RecyclerView.ViewHolder> OnStartDrag;

        protected override void BindData(RssListEditViewHolder holder, RssServiceModel item)
        {
            base.BindData(holder, item);

            holder.TitleTextView.Text = item.Name;
            holder.SubtitleTextView.Text = item.UpdateTime == null
                ? Activity.GetText(Resource.String.rssList_notUpdated)
                : $"{Activity.GetText(Resource.String.rssList_updated)} {item.UpdateTime.Value.ToShortDateLocaleString()}";
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder([NotNull] ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).NotNull().Inflate(Resource.Layout.list_item_rss_edit, parent, false).NotNull();

            var viewHolder = new RssListEditViewHolder(view);

            viewHolder.DeleteImage.Click += (sender, args) =>
            {
                var position = viewHolder.AdapterPosition;
                DeleteClick?.Invoke(sender, Items.ElementAt(position));
            };

            viewHolder.ReorderImage.Touch += (sender, args) =>
            {
                var action = args?.Event?.Action;
                if (action == MotionEventActions.Up || action == MotionEventActions.Down) OnStartDrag?.Invoke(viewHolder);
            };

            return viewHolder;
        }
    }
}
