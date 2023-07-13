using System;
using umvel.challenge.domain.Commons.Rules;

namespace umvel.challenge.domain.Entities.Customers.Rules
{
	public interface ICustomerIdMustExistRule : IRule
	{
		public int CustomerId { get; set; }
	}
}

