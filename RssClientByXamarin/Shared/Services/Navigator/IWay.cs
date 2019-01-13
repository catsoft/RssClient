namespace Droid.Infrastructure
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

    public abstract class Way<TModel, TData> : IWay<TModel, TData>
    {
        public abstract void Go();
        public TData Data { get; set; }
    }
}