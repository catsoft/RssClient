namespace Shared.App.Rss.LoadMessages
{
    public class LoadMessagesRequest
    {
        public RssModel Model { get; set; }

        public LoadMessagesRequest()
        {
            
        }

        public LoadMessagesRequest(RssModel model)
        {
            Model = model;
        }
    }
}
