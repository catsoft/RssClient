namespace Shared.Services.Navigator
{
    public abstract class Way<TData> : IWay<TData>
    {
        public abstract void Go();
        public TData Data { get; set; }
    }
}