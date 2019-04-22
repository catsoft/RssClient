using Autofac;
using Shared.Extensions;
using Shared.ViewModels.Close;

namespace Shared.Infrastructure.Navigation
{
    public class Navigator : INavigator
    {
        public void Go(IWay way) { way.Go(); }

        public void GoBack() { App.Container.Resolve<IWay<CloseViewModel>>().NotNull().Go(); }
    }
}
