using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Widget;

namespace Droid.CustomView
{
    public class DrawableProgressBar : ImageView
    {
        protected DrawableProgressBar(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Init();
        }

        public DrawableProgressBar(Context context) : base(context)
        {
            Init();
        }

        public DrawableProgressBar(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init();
        }

        public DrawableProgressBar(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init();
        }

        public DrawableProgressBar(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr,
            defStyleRes)
        {
            Init();
        }

        private void Init()
        {
            SetScaleType(ScaleType.FitXy);
            SetBackgroundResource(Resource.Drawable.vector_test);
            (Background as AnimatedVectorDrawable)?.Start();
        }
    }
}