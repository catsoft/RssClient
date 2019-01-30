using System.Collections.Generic;
using System.Linq;
using Android.App;

namespace Droid.Screens.Base.Adapters
{
    public abstract class WithItemsAdapter<TItem, TCollection> : WithActivityAdapter
        where TCollection : IEnumerable<TItem>
    {
        public TCollection Items { get; }

        protected WithItemsAdapter(TCollection items, Activity activity) : base(activity)
        {
            Items = items;
        }

        public override int ItemCount => Items.Count();
    }
}