namespace Droid.Screens.Base.Adapters
{
    public interface IDataBind<TItem>
    {
        TItem Item { get; set; }
        void BindData(TItem item);
    }
}
