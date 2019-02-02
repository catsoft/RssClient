using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.NativeExtension;
using Droid.Screens.Base.Adapters;
using Droid.Screens.Base.DragRecyclerView;
using FFImageLoading;
using FFImageLoading.Work;
using RssClient.Repository;
using Shared;
using Shared.Database.Rss;
using Shared.Services.Locale;
using Shared.ViewModels;

namespace Droid.Screens.RssEditList
{
    public class RssListEditAdapter : DataBindAdapter<RssModel, List<RssModel>, RssListEditViewHolder>, IReorderListHelperAdapter
    {
        public event Action<RecyclerView.ViewHolder> OnStartDrag;
        private readonly IRssRepository _rssRepository;
        
        public RssListEditAdapter(IEnumerable<RssModel> items, Activity activity, IRssRepository rssRepository) : base(items.ToList(), activity)
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
                _rssRepository.UpdatePosition(localItem, i);
            }
            
            NotifyItemMoved(fromPosition, toPosition);
        }

        protected override void BindData(RssListEditViewHolder holder, RssModel item)
        {
            base.BindData(holder, item);
            
            var localeService = App.Container.Resolve<ILocale>();

            holder.TitleTextView.Text = item.Name;
            holder.SubtitleTextView.Text = item.UpdateTime == null
                ? Activity.GetText(Resource.String.rssList_notUpdated)
                : $"{Activity.GetText(Resource.String.rssList_updated)} {item.UpdateTime.Value.ToString("g", new CultureInfo(localeService.GetCurrentLocaleId()))}";
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_rss_edit, parent, false);

            var viewHolder = new RssListEditViewHolder(view);

            viewHolder.DeleteImage.Click += (sender, args) =>
            {
                var position = viewHolder.AdapterPosition;
                _rssRepository.Remove(viewHolder.Item);
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