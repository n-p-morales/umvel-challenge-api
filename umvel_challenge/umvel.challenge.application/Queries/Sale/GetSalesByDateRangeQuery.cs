using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using umvel.challenge.application.Queries.Product;
using umvel.challenge.domain.Contexts;
using umvel.challenge.domain.Models.Sales;

namespace umvel.challenge.application.Queries.Sale
{
	public class GetSalesByDateRangeQuery: IRequest<List<SaleModel>>
	{
		public GetSalesByDateRangeQuery(DateTime startDate, DateTime endDate)
		{
			StartDate = startDate;
			EndDate = endDate;
		}

		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}

	public class GetSalesByDateRangeQueryHandler : IRequestHandler<GetSalesByDateRangeQuery, List<SaleModel>>
	{
        private readonly IValidator<GetSalesByDateRangeQuery> validator;
        private readonly IUmvelChallengeDbContext context;

		public GetSalesByDateRangeQueryHandler(
            IValidator<GetSalesByDateRangeQuery> validator,
            IUmvelChallengeDbContext context
			)
		{
			this.validator = validator;
			this.context = context;
		}

		public async Task<List<SaleModel>> Handle(GetSalesByDateRangeQuery request, CancellationToken cancellationToken)
		{
			validator.ValidateAndThrow(request);

			using (context)
			{
				var sales = await context.Sales
					.AsNoTracking()
					.Where(p => p.Date >= request.StartDate && p.Date <= request.EndDate)
					.Select(p => new SaleModel(p))
					.ToListAsync();

				return sales ?? new List<SaleModel>();
			}
		}
    }
}

