using System;
namespace umvel.challenge.application.Exceptions
{
	public class ServiceException : Exception
	{
        public ServiceException(string message, Exception exception, ResponseCode code)
            : base(message, exception)
        {
            Message = message;
            Code = code;
        }

        public ServiceException(string message, ResponseCode code)
            : base(message)
        {
            Message = message;
            Code = code;
        }

        public string Message { get; }

        public ResponseCode Code { get; }

        public override string ToString()
        {
            return $"Service exception: {Message}";
        }
    }
}

