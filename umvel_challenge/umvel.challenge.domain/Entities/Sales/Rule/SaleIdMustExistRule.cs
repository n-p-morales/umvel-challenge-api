using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using umvel.challenge.domain.Contexts;

namespace umvel.challenge.domain.Entities.Sales.Rule
{
	public class SaleIdMustExistRule: ISaleIdMustExistRule
	{
		private readonly IUmvelChallengeDbContext context;
        public SaleIdMustExistRule(IUmvelChallengeDbContext context)
		{
            this.context = context;
        }

        public string Message => $"The sale id {SaleId} does not exist for an relation";

        public int SaleId { get; set; }

        public bool IsValid()
        {
            if (SaleId == 0)
            {
                throw new ArgumentException("The sale id must be greater than 0");
            }

            return context.Sales.AsNoTracking().Any(x => x.SaleId == SaleId);
        }
    }
}

