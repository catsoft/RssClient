using Shared.App.Base.Database;

namespace Shared.App.Base.Command
{
    public interface ICommand<T, in TE>
    where T : BaseResponse
    {
        ILocalDb LocalDatabase { get; set; }
        ICommandDelegate<T> Delegate { get; set; }
        void Execute(TE model);
    }
}
