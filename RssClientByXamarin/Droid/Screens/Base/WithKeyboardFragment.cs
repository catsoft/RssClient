using Droid.Screens.Navigation;

namespace Droid.Screens.Base
{
    public abstract class WithKeyboardFragment : TitleFragment
    {
        public override void OnDetach()
        {
            Activity.HideKeyboard();

            base.OnDetach();
        }
    }
}