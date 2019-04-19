using JetBrains.Annotations;

namespace Shared.Infrastructure.Navigation
{
    public interface INavigator
    {
        void Go([NotNull] IWay way);

        void GoBack();
    }
}
