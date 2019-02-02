using System.Collections.Generic;
using Android.App;
using Droid.Screens.Base.Adapters;
using Droid.Screens.Base.SwipeRecyclerView;

namespace Droid.Screens.Base.SwipeButtonRecyclerView
{
	public abstract class SwipeButtonListAdapter<T, TCollection> : WithItemsAdapter<T, TCollection>, IItemTouchHelperAdapter
		where TCollection : IEnumerable<T>
	{
		public abstract void OnItemDismiss(int position);

		protected SwipeButtonListAdapter(TCollection items, Activity activity) : base(items, activity)
		{

		}
	}
}