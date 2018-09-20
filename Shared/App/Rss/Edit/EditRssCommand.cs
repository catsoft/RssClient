using Android.Content;
using Shared.App.Base.Command;
using Shared.App.Base.Database;

namespace Shared.App.Rss.Edit
{
    public class EditRssCommand : BaseCommand<EditRssResponse, EditRssRequest>
    {
        public EditRssCommand(Context context, ILocalDb localDb, ICommandDelegate<EditRssResponse> commandDelegate) : base(context, localDb, commandDelegate)
        {
        }

        public override void Execute(EditRssRequest model)
        {
            model.Model.Name = model.Name;
            model.Model.Rss = model.Rss;

            LocalDatabase?.UpdateItemByLocalId(model.Model);

            Delegate?.OnSuccess?.Invoke(new EditRssResponse());
        }
    }
}
