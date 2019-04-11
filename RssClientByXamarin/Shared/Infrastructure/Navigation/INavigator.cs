namespace Shared.Infrastructure.Navigation
{
    public interface INavigator
    {
        void Go(IWay way);
        
        void GoBack();
    }
}