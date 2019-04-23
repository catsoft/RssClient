using System.Collections.Generic;
using System.Linq;
using Android.Support.V7.Widget;
using Core.Extensions;
using Droid.Screens.Base.Adapters;
using DynamicData;
using JetBrains.Annotations;

namespace Droid.Infrastructure.Collections
{
    public class AdapterUpdater<T>
        where T : class
    {
        [NotNull] private readonly RecyclerView _recyclerView;
        [NotNull] private readonly WithItemsAdapter<T, IEnumerable<T>> _adapter;
        [NotNull] private readonly SourceList<T> _viewModelSourceList;

        public AdapterUpdater([NotNull] RecyclerView recyclerView, 
            [NotNull] WithItemsAdapter<T, IEnumerable<T>> adapter,
            [NotNull] SourceList<T> viewModelSourceList)
        {
            _recyclerView = recyclerView;
            _adapter = adapter;
            _viewModelSourceList = viewModelSourceList;
        }

        public void Update([NotNull] IChangeSet<T> observableList)
        {
            _adapter.Items = _viewModelSourceList.Items ?? new List<T>();

            foreach (var change in observableList.Where(w => w != null))
                switch (change.Reason)
                {
                    case ListChangeReason.Add:
                        _adapter.NotifyItemInserted(change.Item.CurrentIndex);
                        break;
                    case ListChangeReason.AddRange:
                        _adapter.NotifyItemRangeInserted(change.Range.NotNull().Index, change.Range.NotNull().Count);
                        break;
                    case ListChangeReason.Replace:
                        var viewHolder = _recyclerView.FindViewHolderForAdapterPosition(change.Item.CurrentIndex);
                        if (viewHolder is IDataBind<T> bind) bind.BindData(change.Item.Current.NotNull());
                        break;
                    case ListChangeReason.Remove:
                        _adapter.NotifyItemRemoved(change.Item.CurrentIndex);
                        break;
                    case ListChangeReason.RemoveRange:
                        _adapter.NotifyItemRangeRemoved(change.Range.NotNull().Index, change.Range.NotNull().Count);
                        break;
                    case ListChangeReason.Refresh:
                        _adapter.NotifyItemChanged(change.Item.CurrentIndex);
                        break;
                    case ListChangeReason.Moved:
                        _adapter.NotifyItemMoved(change.Item.PreviousIndex, change.Item.CurrentIndex);
                        break;
                    case ListChangeReason.Clear:
                        _adapter.NotifyDataSetChanged();
                        break;
                }
        }
    }
}
