using Core.Infrastructure.Navigation;
using Core.ViewModels.Donate;
using Droid.Screens.Navigation;
using JetBrains.Annotations;

namespace Droid.Screens.Donate
{
    public class DonateWay : IWay<DonateViewModel>
    {
        [NotNull] private readonly IFragmentManager _fragmentManager;

        public DonateWay([NotNull] IFragmentManager fragmentManager)
        {
            _fragmentManager = fragmentManager;
        }
        
        public void Go()
        {
            _fragmentManager.AddFragment(new DonateFragment());
        }
    }
}