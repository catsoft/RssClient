namespace Shared.Services.Navigator
{
    public abstract class Way<TModel, TData> : IWay<TModel, TData>
    {
        public abstract void Go();
        public TData Data { get; set; }
    }
}