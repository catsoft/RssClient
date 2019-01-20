namespace Shared.Services.Navigator
{
    public interface IWay
    {
        void Go();
    }

    public interface IWay<TModel, TData>
    {
        void Go();
        TData Data { get; set; }
    }
}