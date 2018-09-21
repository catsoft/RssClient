
namespace Shared.App.Rss.Remove
{
    public class RemoveRssRequest
    {
        public RssModel Model { get; set; }

        public RemoveRssRequest()
        {
            
        }

        public RemoveRssRequest(RssModel model)
        {
            Model = model;
        }
    }
}
