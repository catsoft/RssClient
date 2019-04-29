using Android.Views;
using Android.Widget;
using Core.Extensions;
using JetBrains.Annotations;

namespace Droid.Screens.Settings.LoadImages
{
    public class SettingsLoadImagesFragmentViewHolder
    {
        public SettingsLoadImagesFragmentViewHolder([NotNull] View view)
        {
            CheckBox = view.FindViewById<CheckBox>(Resource.Id.checkBox_loadImages_yesOrNo).NotNull();
        }
        
        [NotNull] public CheckBox CheckBox { get; }
    }
}