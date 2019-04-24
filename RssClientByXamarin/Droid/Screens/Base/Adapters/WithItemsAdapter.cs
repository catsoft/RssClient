using System.Collections.Generic;
using System.Linq;
using Android.App;
using JetBrains.Annotations;

namespace Droid.Screens.Base.Adapters
{
    public abstract class WithItemsAdapter<TItem, TCollection> : WithActivityAdapter
        where TCollection : class, IEnumerable<TItem>
    {
        protected WithItemsAdapter([NotNull] TCollection items, [NotNull] Activity activity) : base(activity) { Items = items; }

        [NotNull] [ItemNotNull] public TCollection Items { get; set; }

        public override int ItemCount => Items.Count();
    }
}
