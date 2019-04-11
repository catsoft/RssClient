using Android.Arch.Core.Internal;
using Shared.Infrastructure.ViewModels;

namespace Droid.Screens.Navigation
{
    //TODO чота написать
    public abstract class SubFragment : BaseFragment<ViewModel>
    {
        public override bool IsRoot => false;
    }
}