using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.Settings.Pushes
{
    public class SettingsPushesFragmentViewHolder
    {
        public SettingsPushesFragmentViewHolder([NotNull] View view)
        {
            CheckBox = view.FindNotNull<CheckBox>(Resource.Id.checkBox_pushes_yesOrNo);
        }
        
        [NotNull] public CheckBox CheckBox { get; }
    }
}