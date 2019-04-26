using Core.Infrastructure.Navigation;
using Core.ViewModels.AnimationWeaver;
using Droid.Screens.Navigation;

namespace Droid.Screens.AnimationWeaver
{
    public class AnimationWeaverWay : IWay<AnimationWeaverViewModel>
    {
        private readonly IFragmentManager _fragmentManager;


        public AnimationWeaverWay(IFragmentManager fragmentManager)
        {
            _fragmentManager = fragmentManager;
        }
        
        public void Go()
        {
            _fragmentManager.AddFragment(new AnimationWeaverFragment());
        }
    }
}