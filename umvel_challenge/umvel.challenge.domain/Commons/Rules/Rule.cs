using System;
using umvel.challenge.domain.Commons.Rules.Exceptions;

namespace umvel.challenge.domain.Commons.Rules
{
	public abstract class Rule
	{
        protected static void ValidateRule(IRule rule)
        {
            if (!rule.IsValid())
            {
                throw new InvalidRuleException(rule);
            }
        }
    }
}

