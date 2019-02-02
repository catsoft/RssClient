using System.Collections.Generic;
using Android.App;
using Droid.Screens.Base.Adapters;

namespace Droid.Screens.Base.DragRecyclerView
{
    public abstract class ReorderRecyclerViewAdapter<TItems, TCollection> : WithItemsAdapter<TItems, TCollection>, IReorderListHelperAdapter
        where TCollection : IEnumerable<TItems>
    {
        protected ReorderRecyclerViewAdapter(TCollection items, Activity activity) : base(items, activity)
        {
        }

        public abstract void OnMove(int fromPosition, int toPosition);
    }
}