using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using umvel.challenge.domain.Contexts;

namespace umvel.challenge.domain.Entities.Products.Rules
{
	public class ProductIdMustExistRule: IProductIdMustExistRule
	{
        private readonly IUmvelChallengeDbContext context;

        public ProductIdMustExistRule(IUmvelChallengeDbContext context)
		{
			this.context = context;
		}

        public string Message => $"The product id {ProductId} does not exist for an relation";

        public int ProductId { get; set; }

        public bool IsValid()
        {
            if (ProductId == 0)
            {
                throw new ArgumentException("The product id must be greater than 0");
            }

            return context.Products.AsNoTracking().Any(x => x.ProductId == ProductId);
        }
    }
}

