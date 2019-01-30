namespace Shared.Services.Navigator
{
    public abstract class DataWay<TData> : IWay
    {
        public abstract void Go();
        public TData Data { get; set; }
    }
}