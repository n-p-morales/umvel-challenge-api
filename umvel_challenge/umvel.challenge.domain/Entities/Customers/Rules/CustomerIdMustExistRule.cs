using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using umvel.challenge.domain.Contexts;

namespace umvel.challenge.domain.Entities.Customers.Rules
{
	public class CustomerIdMustExistRule: ICustomerIdMustExistRule
	{
		private readonly IUmvelChallengeDbContext context;

		public CustomerIdMustExistRule(IUmvelChallengeDbContext context)
		{
			this.context = context;
		}

		public string Message => $"The customer id {CustomerId} does not exist for an relation";

		public int CustomerId { get; set; }

		public bool IsValid()
		{
			if (CustomerId == 0)
			{
				throw new ArgumentException("The customer id must be greater than 0");
			}

			return context.Customers.AsNoTracking().Any(x => x.CustomerId == CustomerId);
		}
	}
}

