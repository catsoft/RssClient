using System;
using System.Collections.Generic;
using Android.App;
using Droid.Screens.Base.Adapters;

namespace Droid.Screens.Base.SwipeButtonRecyclerView
{
	public abstract class SwipeButtonListAdapter<T, TCollection> : WithItemsAdapter<T, TCollection>, ISwipeButtonItemTouchHelperAdapter
		where TCollection : IEnumerable<T>
	{
		public abstract bool IsLeftButton { get; }
		public abstract  bool IsRightButton { get; }
		public abstract  string LeftButtonText { get; }
		public abstract  string RightButtonText { get; }
		
		public event Action LeftButtonAction;
		public event Action RightButtonAction;
        
		public void OnLeftButton()
		{
			LeftButtonAction?.Invoke();   
		}
		
		public void OnRightButton()
		{
			RightButtonAction?.Invoke();
		}

		protected SwipeButtonListAdapter(TCollection items, Activity activity) : base(items, activity)
		{

		}
	}
}