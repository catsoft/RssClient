namespace Shared.App.Base.Command
{
    public class Error
    {
        public Error()
        {

        }

        public Error(string errorCode, string errorMessage)
        {
            Code = errorCode;
            Message = errorMessage;
        }

        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Code message
        /// </summary>
        public string Code { get; set; }
    }
}
