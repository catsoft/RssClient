namespace Shared.Services.Navigator
{
    public interface INavigator
    {
        void Go(IWay way);
        void Go<T, TD>(IWay<T, TD> way);
        void GoBack();
    }
}
