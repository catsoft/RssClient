using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Core.Extensions;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.Settings.AutoUpdate
{
    public class SettingsAutoUpdateFragmentViewHolder
    {
        public SettingsAutoUpdateFragmentViewHolder([NotNull] View view)
        {
            CheckBox = view.FindNotNull<CheckBox>(Resource.Id.checkBox_autoUpdate_yesOrNo);
            IntervalTextInputLayout = view.FindNotNull<TextInputLayout>(Resource.Id.textInputLayout_autoUpdate_interval);
        }
        
        [NotNull] public CheckBox CheckBox { get; }
        
        [NotNull] public TextInputLayout IntervalTextInputLayout { get; }

        [NotNull] public EditText EditTextInterval => IntervalTextInputLayout.EditText.NotNull();
    }
}