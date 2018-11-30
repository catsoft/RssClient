using Shared.App.Base.Command;

namespace Shared.App.Rss.Remove
{
    public class RemoveRssResponse : BaseResponse
    {
        public RemoveRssResponse()
        {
            
        }

        public RemoveRssResponse(RssModel model)
        {
            Model = model;
        }

        public RssModel Model { get; set; }
    }
}
