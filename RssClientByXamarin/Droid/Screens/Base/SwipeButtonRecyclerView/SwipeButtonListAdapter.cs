using System.Collections.Generic;
using Android.App;
using Droid.Screens.Base.Adapters;

namespace Droid.Screens.Base.SwipeButtonRecyclerView
{
	public abstract class SwipeButtonListAdapter<T, TCollection> : WithItemsAdapter<T, TCollection>
		where TCollection : IEnumerable<T>
	{
		protected SwipeButtonListAdapter(TCollection items, Activity activity) : base(items, activity)
		{

		}
	}
}