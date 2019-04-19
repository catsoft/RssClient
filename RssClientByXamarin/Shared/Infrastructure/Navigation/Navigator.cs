#region

using Autofac;
using Shared.ViewModels.Close;

#endregion

namespace Shared.Infrastructure.Navigation
{
    public class Navigator : INavigator
    {
        public void Go(IWay way) { way.Go(); }

        public void GoBack()
        {
            var goBackWay = App.Container.Resolve<IWay<CloseViewModel>>();
            goBackWay.Go();
        }
    }
}
