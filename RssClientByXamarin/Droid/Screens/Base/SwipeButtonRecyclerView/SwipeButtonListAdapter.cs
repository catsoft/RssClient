using System.Collections.Generic;
using Android.App;
using Droid.Screens.Base.Adapters;
using Droid.Screens.Base.SwipeRecyclerView;

namespace Droid.Screens.Base.SwipeButtonRecyclerView
{
	public abstract class SwipeButtonListAdapter<T, TCollection> : WithItemsAdapter<T, TCollection>, ISwipeButtonItemTouchHelperAdapter
		where TCollection : IEnumerable<T>
	{

		protected SwipeButtonListAdapter(TCollection items, Activity activity) : base(items, activity)
		{

		}
	}
}