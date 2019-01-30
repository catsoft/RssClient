namespace Shared.Services.Navigator
{
    public interface IWay
    {
        void Go();
    }

    public interface IWay<TData>
    {
        void Go();
        TData Data { get; set; }
    }
}