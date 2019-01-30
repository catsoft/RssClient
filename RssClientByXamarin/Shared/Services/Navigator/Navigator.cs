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

        public void Go<T>(IWay<T> way)
        {
            way.Go();
        }

        public void GoBack()
        {
            var goBackWay = App.Container.Resolve<IWay<CloseViewModel.Way.WayData>>();
            goBackWay.Go();
        }
    }
}