using System;
namespace umvel.challenge.domain.Commons.Rules
{
	public interface IRule
	{
        string Message { get; }

        bool IsValid();
    }
}

