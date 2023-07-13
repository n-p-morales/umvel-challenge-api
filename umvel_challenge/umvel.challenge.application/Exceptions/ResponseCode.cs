using System;
namespace umvel.challenge.application.Exceptions
{
	public enum ResponseCode
	{
 
        Success = 0,

        ServiceError = 1000,

        InvalidToken = 1001,

        ExpiredToken = 1002,

        NoDataAvailable = 1003,

        DuplicatedData = 1005,

        ValidationError = 2000,

        RuleError = 3000,

        InternalError = 9999,
    }
}

