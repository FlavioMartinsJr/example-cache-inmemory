using Produto.Domain.Enums;

namespace Produto.Application.Exceptions
{
    public class UserMessageException : Exception
    {
        public string UserMessage { get; set; }
        public ErrorCode ErrorCode { get; set; }

        public UserMessageException(ErrorCode errorCode, string userMessage, Exception? innerException = null) : base(userMessage, innerException)
        {
            ErrorCode = errorCode;
            UserMessage = userMessage;
        }
        public UserMessageException(string message, string userMessage, Exception? innerException = null) : base(message, innerException)
        {
            UserMessage = userMessage;
        }
        public UserMessageException(ErrorCode errorCode, string message, string userMessage, Exception? innerException = null) : base(message, innerException)
        {
            ErrorCode = errorCode;
            UserMessage = userMessage;
        }
    }
}
