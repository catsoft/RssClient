namespace Droid.Screens.Base.Adapters
{
    public interface IDataBind<TItem>
    {
        void BindData(TItem item);
        TItem Item { get; set; }
    }
}