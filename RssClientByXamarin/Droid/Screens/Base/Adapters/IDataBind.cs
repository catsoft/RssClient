using JetBrains.Annotations;

namespace Droid.Screens.Base.Adapters
{
    public interface IDataBind<TItem>
    {
        [CanBeNull] TItem Item { get;  }
        void BindData([NotNull] TItem item);
    }
}
