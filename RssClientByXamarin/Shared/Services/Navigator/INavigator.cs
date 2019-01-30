namespace Shared.Services.Navigator
{
    public interface INavigator
    {
        void Go(IWay way);
        void Go<T>(IWay<T> way);
        void GoBack();
    }
}
