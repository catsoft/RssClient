using System;
using Android.Support.Transitions;
using Android.Views;
using Core.Configuration.Settings;

namespace Droid.NativeExtension
{
    public static class VisibilityExtension
    {
        public static Visibility ToVisibility(this AnimationType type)
        {
            switch (type)
            {
                case AnimationType.None:
                    return null;
                case AnimationType.Fade:
                    return new Fade();
                case AnimationType.Bottom:           
                    return new Slide() {SlideEdge = (int) GravityFlags.Bottom };
                case AnimationType.Top:
                    return new Slide() {SlideEdge = (int) GravityFlags.Top };
                case AnimationType.Left:
                    return new Slide() {SlideEdge = (int) GravityFlags.Left };
                case AnimationType.Right:
                    return new Slide() {SlideEdge = (int) GravityFlags.Right };
                case AnimationType.Explode:
                    return new Explode();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}