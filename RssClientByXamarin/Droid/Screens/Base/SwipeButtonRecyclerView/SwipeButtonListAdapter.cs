using System.Collections.Generic;
using Android.App;
using Android.Runtime;
using Droid.Screens.Base.Adapters;
using Droid.Screens.RssList;

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