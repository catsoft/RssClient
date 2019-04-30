using Android.App;
using Android.Views;
using Core.Extensions;
using JetBrains.Annotations;

namespace Droid.NativeExtension
{
    public static class FindExtension
    {
        [NotNull]
        public static T FindNotNull<T>([NotNull] this View view, int id)
            where T : View
        {
            return view.FindViewById<T>(id).NotNull();
        }
        
        [NotNull]
        public static T FindNotNull<T>([NotNull] this Activity view, int id)
            where T : View
        {
            return view.FindViewById<T>(id).NotNull();
        }
    }
}