using Shared.App.Base.Command;
namespace Shared.App.Rss.List.GetListCommand
{
    public class GetListResponse : BaseResponse
    {
        public RssModel[] Models { get; set; }
    }
}
