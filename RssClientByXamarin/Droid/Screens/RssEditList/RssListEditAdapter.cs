using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Droid.Screens.Base.Adapters;
using Droid.Screens.Base.DragRecyclerView;
using Shared.Infrastructure.Locale;
using Shared.Repository.Rss;
using Shared.Services.Rss;

namespace Droid.Screens.RssEditList
{
    public class RssListEditAdapter : DataBindAdapter<RssDomainModel, List<RssDomainModel>, RssListEditViewHolder>, IReorderListHelperAdapter
    {
        public event Action<RecyclerView.ViewHolder> OnStartDrag;
        private readonly IRssService _rssRepository;
        
        public RssListEditAdapter(IEnumerable<RssDomainModel> items, Activity activity, IRssService rssRepository) : base(items.ToList(), activity)
        {
            _rssRepository = rssRepository;
        }

        public void OnMove(int fromPosition, int toPosition)
        {
            var item = Items[fromPosition];
            Items.RemoveAt(fromPosition);
            Items.Insert(toPosition, item);

            for (var i = 0; i < Items.Count; i++)
            {
                var localItem = Items[i];
                _rssRepository.UpdatePositionAsync(localItem.Id, i);
            }
            
            NotifyItemMoved(fromPosition, toPosition);
        }

        protected override void BindData(RssListEditViewHolder holder, RssDomainModel item)
        {
            base.BindData(holder, item);
            
            holder.TitleTextView.Text = item.Name;
            holder.SubtitleTextView.Text = item.UpdateTime == null
                ? Activity.GetText(Resource.String.rssList_notUpdated)
                : $"{Activity.GetText(Resource.String.rssList_updated)} {item.UpdateTime.Value.ToShortDateLocaleString()}";
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_rss_edit, parent, false);

            var viewHolder = new RssListEditViewHolder(view);

            viewHolder.DeleteImage.Click += (sender, args) =>
            {
                var position = viewHolder.AdapterPosition;
                _rssRepository.RemoveAsync(viewHolder.Item.Id);
                Items.RemoveAt(position);
                NotifyItemRemoved(position);
            };

            viewHolder.ReorderImage.Touch += (sender, args) =>
            {
                if (args.Event.Action == MotionEventActions.Up || args.Event.Action == MotionEventActions.Down)
                {
                    OnStartDrag?.Invoke(viewHolder);
                }
            };
            
            return viewHolder;
        }
    }
}