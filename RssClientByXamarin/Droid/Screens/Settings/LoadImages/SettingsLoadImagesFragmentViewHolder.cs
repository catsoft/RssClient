using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.Settings.LoadImages
{
    public class SettingsLoadImagesFragmentViewHolder
    {
        public SettingsLoadImagesFragmentViewHolder([NotNull] View view)
        {
            CheckBox = view.FindNotNull<CheckBox>(Resource.Id.checkBox_loadImages_yesOrNo);
        }
        
        [NotNull] public CheckBox CheckBox { get; }
    }
}