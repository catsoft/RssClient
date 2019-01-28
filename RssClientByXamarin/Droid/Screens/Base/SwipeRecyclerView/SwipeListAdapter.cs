using System.Collections.Generic;
using Android.App;
using Droid.Screens.Base.Adapters;

namespace Droid.Screens.Base.SwipeRecyclerView
{
	public abstract class SwipeListAdapter<T, TCollection>: WithItemsAdapter<T, TCollection>, IItemTouchHelperAdapter
		where TCollection : IEnumerable<T>
	{
		public abstract void OnItemDismiss(int position);
		
		protected SwipeListAdapter(TCollection items, Activity activity) : base(items, activity)
		{
			
		}
	}
}