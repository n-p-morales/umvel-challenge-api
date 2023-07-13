using System;
using umvel.challenge.domain.Commons.Rules;
using umvel.challenge.domain.Commons.Rules.Exceptions;

namespace umvel.challenge.domain.Commons.EntitiesBase
{
	public class Aggregate
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

