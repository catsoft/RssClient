namespace Shared.App.Base.Command
{
    public class Error
    {
        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Служебное сообщение
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
