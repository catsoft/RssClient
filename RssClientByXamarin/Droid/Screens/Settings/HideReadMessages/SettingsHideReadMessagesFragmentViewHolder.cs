using Android.Views;
using Android.Widget;
using Core.Extensions;
using JetBrains.Annotations;

namespace Droid.Screens.Settings.HideReadMessages
{
    public class SettingsHideReadMessagesFragmentViewHolder
    {
        public SettingsHideReadMessagesFragmentViewHolder([NotNull] View view)
        {
            CheckBox = view.FindViewById<CheckBox>(Resource.Id.checkBox_ReadMessages_hide).NotNull();
        }
        
        [NotNull] public CheckBox CheckBox { get; }
    }
}