using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Core.Extensions;
using JetBrains.Annotations;

namespace Droid.Screens.AnimationWeaver
{
    public class AnimationWeaverFragmentViewHolder
    {
        public AnimationWeaverFragmentViewHolder([NotNull] View view)
        {
            Container = view.FindViewById<LinearLayout>(Resource.Id.linearLayout_animationWeaver_container);

            EnterSpinner = view.FindViewById<AppCompatSpinner>(Resource.Id.spinner_animationWeaver_enterAnim).NotNull();
            ExitSpinner = view.FindViewById<AppCompatSpinner>(Resource.Id.spinner_animationWeaver_exitAnim).NotNull();
            SpeedSpinner = view.FindViewById<AppCompatSpinner>(Resource.Id.spinner_animationWeaver_speedAnim).NotNull();

            RadioGroup = view.FindViewById<RadioGroup>(Resource.Id.radioGroup_animationWeaver_delay).NotNull();
            RadioButtonDelay = view.FindViewById<RadioButton>(Resource.Id.radioButton_animationWeaver_delay).NotNull();
            RadioButtonNotDelay = view.FindViewById<RadioButton>(Resource.Id.radioButton_animationWeaver_withoutDelay).NotNull();
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