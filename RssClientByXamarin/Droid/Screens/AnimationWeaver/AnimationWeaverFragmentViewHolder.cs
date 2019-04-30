using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.AnimationWeaver
{
    public class AnimationWeaverFragmentViewHolder
    {
        public AnimationWeaverFragmentViewHolder([NotNull] View view)
        {
            Container = view.FindNotNull<LinearLayout>(Resource.Id.linearLayout_animationWeaver_container);

            EnterSpinner = view.FindNotNull<AppCompatSpinner>(Resource.Id.spinner_animationWeaver_enterAnim);
            ExitSpinner = view.FindNotNull<AppCompatSpinner>(Resource.Id.spinner_animationWeaver_exitAnim);
            SpeedSpinner = view.FindNotNull<AppCompatSpinner>(Resource.Id.spinner_animationWeaver_speedAnim);

            RadioGroup = view.FindNotNull<RadioGroup>(Resource.Id.radioGroup_animationWeaver_delay);
            RadioButtonDelay = view.FindNotNull<RadioButton>(Resource.Id.radioButton_animationWeaver_delay);
            RadioButtonNotDelay = view.FindNotNull<RadioButton>(Resource.Id.radioButton_animationWeaver_withoutDelay);
        }
        
        [NotNull] public LinearLayout Container { get; }
        
        [NotNull] public AppCompatSpinner EnterSpinner { get; }
        
        [NotNull] public AppCompatSpinner ExitSpinner { get; }
        
        [NotNull] public AppCompatSpinner SpeedSpinner { get; }
        
        [NotNull] public RadioGroup RadioGroup { get; }
        
        [NotNull] public RadioButton RadioButtonDelay { get; }

        [NotNull] public RadioButton RadioButtonNotDelay { get; }
    }
}