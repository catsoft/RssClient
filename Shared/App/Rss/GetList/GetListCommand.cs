using System.Linq;
using Shared.App.Base.Command;
using Shared.App.Base.Database;

namespace Shared.App.Rss.List.GetListCommand
{
    public class GetListCommand : BaseCommand<GetListResponse, GetListRequest>
    {
        public GetListCommand(ILocalDb localDb, ICommandDelegate<GetListResponse> commandDelegate) : base(localDb, commandDelegate)
        {
        }

        public override void Execute(GetListRequest model)
        {
            var response = new GetListResponse()
            {
                Models = LocalDatabase.GetItems<RssModel>()?.ToArray(),
            };

            CommonExecute(response);
        }
    }
}
