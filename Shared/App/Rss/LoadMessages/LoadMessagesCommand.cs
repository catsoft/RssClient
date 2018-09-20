using System;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using Android.Content;
using RssClient.App.Rss.Detail;
using Shared.App.Base.Command;
using Shared.App.Base.Database;

namespace Shared.App.Rss.LoadMessages
{
    public class LoadMessagesCommand : BaseCommand<LoadMessagesResponse, LoadMessagesRequest>
    {
        public LoadMessagesCommand(Context context, ILocalDb localDb, ICommandDelegate<LoadMessagesResponse> commandDelegate) : base(context, localDb, commandDelegate)
        {
        }

        public override void Execute(LoadMessagesRequest model)
        {
            try
            {
                var xmlReader = XmlReader.Create(model.Model.Rss);
                var feed = SyndicationFeed.Load(xmlReader);
                var messages = feed?.Items?.Select(w => new RssMessageModel(w, model.Model.Id)).ToList();

                if (messages?.Any() == true)
                {
                    var oldMessages = LocalDatabase.GetItems<RssMessageModel>()?.Where(w => w.PrimaryKeyRssModel == model.Model.Id);
                    LocalDatabase.DeleteItemsByLocalId(oldMessages);

                    LocalDatabase?.AddNewItems(messages);
                }
            }
            catch (Exception e)
            {
                Delegate?.OnNotConnection?.Invoke();
            }


            Delegate?.OnSuccess?.Invoke(new LoadMessagesResponse());
        }
    }
}
