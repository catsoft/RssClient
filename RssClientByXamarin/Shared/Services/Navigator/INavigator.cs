namespace Shared.Services.Navigator
{
    public interface INavigator
    {
        void Go(IWay way);
        void GoBack();
    }
}