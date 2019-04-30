using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.Settings.HideReadMessages
{
    public class SettingsHideReadMessagesFragmentViewHolder
    {
        public SettingsHideReadMessagesFragmentViewHolder([NotNull] View view)
        {
            CheckBox = view.FindNotNull<CheckBox>(Resource.Id.checkBox_ReadMessages_hide);
        }
        
        [NotNull] public CheckBox CheckBox { get; }
    }
}