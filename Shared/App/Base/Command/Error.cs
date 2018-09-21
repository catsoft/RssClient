namespace Shared.App.Base.Command
{
    public class Error
    {
        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Code message
        /// </summary>
        public string Code { get; set; }

        public Error(string errorCode, string errorMessage)
        {
            Code = errorCode;
            Message = errorMessage;
        }

        public Error()
        {
            
        }
    }
}
