namespace Shared.App.Base.Command
{
    public class BaseResponse
    {

        public bool IsSuccess { get; set; } = true;

        public Error Error { get; set; }
    }
}
