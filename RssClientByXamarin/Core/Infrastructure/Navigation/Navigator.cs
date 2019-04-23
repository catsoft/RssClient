using Autofac;
using Core.Extensions;
using Core.ViewModels.Close;

namespace Core.Infrastructure.Navigation
{
    public class Navigator : INavigator
    {
        public void Go(IWay way) { way.Go(); }

        public void GoBack() { App.Container.Resolve<IWay<CloseViewModel>>().NotNull().Go(); }
    }
}
