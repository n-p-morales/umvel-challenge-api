using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using umvel.challenge.domain.Contexts;
using umvel.challenge.domain.Models.Customers;

namespace umvel.challenge.application.Queries.Customer
{
	public class GetCustomerByCustomerIdQuery: IRequest<CustomerModel>
	{
		public GetCustomerByCustomerIdQuery(int customerId)
		{
			CustomerId = customerId;
		}

		public int CustomerId { get; set; }
	}

	public class GetCustomerByCustomerIdQueryHandler : IRequestHandler<GetCustomerByCustomerIdQuery, CustomerModel>
	{
		private readonly IValidator<GetCustomerByCustomerIdQuery> validator;
		private readonly IUmvelChallengeDbContext context;

		public GetCustomerByCustomerIdQueryHandler(
            IValidator<GetCustomerByCustomerIdQuery> validator,
            IUmvelChallengeDbContext context
            )
		{
			this.validator = validator;
			this.context = context;
		}

		public async Task<CustomerModel> Handle(GetCustomerByCustomerIdQuery request, CancellationToken cancellationToken)
		{
			validator.ValidateAndThrow(request);

			using (context)
			{
				var customer = await context.Customers
					.AsNoTracking()
					.Where(p => p.CustomerId == request.CustomerId)
					.Select(p => new CustomerModel(p))
					.FirstOrDefaultAsync();

				return customer ?? new CustomerModel();
			}
		}
	}

	public class GetCustomerByCustomerIdQueryValidator : AbstractValidator<GetCustomerByCustomerIdQuery>
	{
		public GetCustomerByCustomerIdQueryValidator()
		{
			RuleFor(c => c.CustomerId).GreaterThan(0).WithMessage("CustomerId must be greater than 0");
		}
	}
}

