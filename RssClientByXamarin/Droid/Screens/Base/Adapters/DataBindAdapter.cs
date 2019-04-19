#region

using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Support.V7.Widget;

#endregion

namespace Droid.Screens.Base.Adapters
{
    public abstract class DataBindAdapter<TItem, TCollection, TViewHolder> : WithItemsAdapter<TItem, TCollection>
        where TCollection : class, IEnumerable<TItem>
        where TViewHolder : RecyclerView.ViewHolder, IDataBind<TItem>
    {
        protected DataBindAdapter(TCollection items, Activity activity) : base(items, activity) { }

        protected virtual void BindData(TViewHolder holder, TItem item) { }

        public sealed override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = Items.ElementAt(position);

            if (holder is TViewHolder bindData)
            {
                bindData.BindData(item);
                BindData(bindData, item);
            }
        }
    }
}
