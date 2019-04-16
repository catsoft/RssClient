using System;
using System.Collections.Generic;
using Android.App;
using Android.Support.V7.Widget;
using Droid.Screens.Base.Adapters;
using DynamicData;
using Shared.Services.Rss;

namespace Droid.Infrastructure.Collections
{
    public class AdapterUpdater<T>
    {
        private readonly Activity _activity;
        private readonly SourceList<T> _viewModelSourceList;
        private readonly WithItemsAdapter<T, IEnumerable<T>> _adapter;

        public AdapterUpdater(WithItemsAdapter<T, IEnumerable<T>> adapter, SourceList<T> viewModelSourceList)
        {
            _adapter = adapter;
            _viewModelSourceList = viewModelSourceList;
        }

        public void Update(IChangeSet<RssServiceModel> observableList)
        {
            _adapter.Items = _viewModelSourceList.Items;
        }
    }
}