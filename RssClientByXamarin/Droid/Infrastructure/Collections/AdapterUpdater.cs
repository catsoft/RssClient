using System.Collections.Generic;
using Droid.Screens.Base.Adapters;
using DynamicData;
using Shared.Services.Rss;

namespace Droid.Infrastructure.Collections
{
    public class AdapterUpdater<T>
    {
        private readonly SourceList<T> _viewModelSourceList;
        private readonly WithItemsAdapter<T, IEnumerable<T>> _adapter;

        public AdapterUpdater(WithItemsAdapter<T, IEnumerable<T>> adapter, SourceList<T> viewModelSourceList)
        {
            _adapter = adapter;
            _viewModelSourceList = viewModelSourceList;
        }

        public void Update(IChangeSet<T> observableList)
        {
            _adapter.Items = _viewModelSourceList.Items;
            
            foreach (var change in observableList)
            {
                switch (change.Reason)
                {
                    case ListChangeReason.Add:
                        _adapter.NotifyItemInserted(change.Item.CurrentIndex);
                        break;
                    case ListChangeReason.AddRange:
                        _adapter.NotifyItemRangeInserted(change.Range.Index, change.Range.Count);
                        break;
                    case ListChangeReason.Replace:
                        _adapter.NotifyItemChanged(change.Item.CurrentIndex);
                        break;
                    case ListChangeReason.Remove:
                        _adapter.NotifyItemRemoved(change.Item.CurrentIndex);
                        break;
                    case ListChangeReason.RemoveRange:
                        _adapter.NotifyItemRangeRemoved(change.Range.Index, change.Range.Count);
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
}