using Android.Content;
using Shared.App.Base.Command;
using Shared.App.Base.Database;

namespace Shared.App.Rss.Remove
{
    public class RemoveRssCommand : BaseCommand<RemoveRssResponse, RemoveRssRequest>
    {
        public RemoveRssCommand(ILocalDb localDb, ICommandDelegate<RemoveRssResponse> commandDelegate) : base(localDb, commandDelegate)
        {
        }

        public override void Execute(RemoveRssRequest model)
        {
            LocalDatabase?.DeleteItemByLocalId(model.Model);

            Delegate?.OnSuccess?.Invoke(new RemoveRssResponse());
        }
    }
}
