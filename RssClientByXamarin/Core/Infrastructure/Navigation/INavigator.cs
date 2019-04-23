using JetBrains.Annotations;

namespace Core.Infrastructure.Navigation
{
    public interface INavigator
    {
        void Go([NotNull] IWay way);

        void GoBack();
    }
}
