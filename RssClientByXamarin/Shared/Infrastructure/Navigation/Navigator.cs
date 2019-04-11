using Autofac;
using Shared.ViewModels.Close;

namespace Shared.Infrastructure.Navigation
{
    public class Navigator : INavigator
    {
        public void Go(IWay way)
        {
            way.Go();
        }

        public void GoBack()
        {
            var goBackWay = App.Container.Resolve<CloseViewModel.Way>();
            goBackWay.Go();
        }
    }
}