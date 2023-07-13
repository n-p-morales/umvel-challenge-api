using System;
namespace umvel.challenge.domain.Commons.Rules.Exceptions
{
	public class InvalidRuleException: Exception
	{
        public InvalidRuleException(IRule rule)
           : base(rule.Message)
        {
            Details = rule.Message;
            Rule = rule;
        }

        public string Details { get; }

        public IRule Rule { get; }

        public override string ToString()
        {
            return $"{Rule.GetType().FullName}: {Rule.Message}";
        }
    }
}

