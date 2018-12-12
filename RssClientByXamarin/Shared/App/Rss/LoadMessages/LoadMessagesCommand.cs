//using System;
//using System.IO;
//using System.Linq;
//using System.Net.Http;
//using System.ServiceModel.Syndication;
//using System.Xml;
//using Shared.App.Base.Command;
//using Shared.App.Base.Database;

//namespace Shared.App.Rss.LoadMessages
//{
//    public class LoadMessagesCommand : BaseCommand<LoadMessagesResponse, LoadMessagesRequest>
//    {
//        public LoadMessagesCommand(ILocalDb localDb, ICommandDelegate<LoadMessagesResponse> commandDelegate) : base(localDb, commandDelegate)
//        {
//        }

//        public override void Execute(LoadMessagesRequest model)
//        {
//            try
//            {
//                var httpClient = new HttpClient();
//                var response = httpClient.GetAsync(model.Model.Rss);
//                response.Wait();

//                var stream = response.Result.Content.ReadAsStreamAsync();

//                var xmlReader = XmlReader.Create(stream.Result);
//                var feed = SyndicationFeed.Load(xmlReader);
//                var messages = feed?.Items?.Select(w => new RssMessageModel(w, model.Model.Id)).ToList();

//                if (messages?.Any() == true)
//                {
//                    var oldMessages = LocalDatabase.GetItems<RssMessageModel>()
//                        ?.Where(w => w.PrimaryKeyRssModel == model.Model.Id);
//                    LocalDatabase.DeleteItemsByLocalId(oldMessages);

//                    LocalDatabase?.AddNewItems(messages);
//                }
//            }
//            catch (Exception e)
//            {
//                Delegate?.OnFailed?.Invoke(new Error(e.Message, e.Message));
//            }

//            Delegate?.OnSuccess?.Invoke(new LoadMessagesResponse());
//        }
//    }
//}
