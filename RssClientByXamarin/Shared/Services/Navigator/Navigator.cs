using Autofac;
using Shared;
using Shared.Services.Navigator;
using Shared.ViewModels;

namespace Droid.Infrastructure
{
    public class Navigator : INavigator
    {
        public void Go(IWay way)
        {
            way.Go();
        }

        public void Go<T, TD>(IWay<T, TD> way)
        {
            way.Go();
        }

        public void GoBack()
        {
            var goBackWay = App.Container.Resolve<IWay<CloseViewModel, CloseViewModel.CloseWay.CloseWayData>>();
            goBackWay.Go();
        }
    }
}