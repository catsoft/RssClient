using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Support.V7.Widget;
using JetBrains.Annotations;

namespace Droid.Screens.Base.Adapters
{
    public abstract class DataBindAdapter<TItem, TCollection, TViewHolder> : WithItemsAdapter<TItem, TCollection>
        where TCollection : class, IEnumerable<TItem>
        where TViewHolder : RecyclerView.ViewHolder, IDataBind<TItem>
    {
        protected DataBindAdapter([NotNull] TCollection items, [NotNull] Activity activity) : base(items, activity) { }

        public sealed override void OnBindViewHolder([NotNull] RecyclerView.ViewHolder holder, int position)
        {
            var item = Items.ElementAt(position);

            if (item != null && holder is TViewHolder bindData) bindData.BindData(item);
        }
    }
}
