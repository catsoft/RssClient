using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
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