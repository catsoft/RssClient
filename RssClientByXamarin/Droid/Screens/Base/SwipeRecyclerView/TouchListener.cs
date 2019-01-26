using System;
using Android.Views;
using Object = Java.Lang.Object;

namespace Droid.Screens.Base.SwipeRecyclerView
{
    public class TouchListener : Object, View.IOnTouchListener
    {
        private readonly Func<View, MotionEvent, bool> _onTouch;

        public TouchListener(Func<View, MotionEvent, bool> onTouch)
        {
            this._onTouch = onTouch;
        }

        public bool OnTouch(View v, MotionEvent motionEvent)
        {
            return _onTouch(v, motionEvent);
        }
    }
}