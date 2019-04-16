using System.Collections.Generic;
using System.Linq;
using Android.App;

namespace Droid.Screens.Base.Adapters
{
    public abstract class WithItemsAdapter<TItem, TCollection> : WithActivityAdapter
        where TCollection : class, IEnumerable<TItem>
    {
        private TCollection _items;

        public TCollection Items
        {
            get => _items;
            set
            {
                if (_items != value)
                {
                    _items = value;
                    NotifyDataSetChanged();
                }
            }
        }

        protected WithItemsAdapter(TCollection items, Activity activity) : base(activity)
        {
            Items = items;
        }

        public override int ItemCount => Items.Count();
    }
}