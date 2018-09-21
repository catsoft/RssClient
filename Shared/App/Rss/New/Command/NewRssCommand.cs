using System;
using Shared.App.Base.Command;
using Shared.App.Base.Database;

namespace Shared.App.Rss.New.Command
{
    public class NewRssCommand : BaseCommand<NewRssResponse, NewRssRequest>
    {
        public NewRssCommand(ILocalDb localDb, ICommandDelegate<NewRssResponse> commandDelegate) : base(localDb, commandDelegate)
        {
        }

        public override void Execute(NewRssRequest model)
        {
            var newItem = new RssModel(model.Name, model.Rss, DateTime.Now);
            LocalDatabase?.AddNewItem(newItem);

            Delegate?.OnSuccess?.Invoke(new NewRssResponse());
        }
    }
}
