using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using umvel.challenge.application.Commands.Products;
using umvel.challenge.application.Exceptions;
using umvel.challenge.domain.Aggregates;
using umvel.challenge.domain.Contexts;
using umvel.challenge.domain.Models.Sales;

namespace umvel.challenge.application.Commands.Sales
{
	public class CreateSaleCommand: IRequest<SaleModel>
	{
		public CreateSaleCommand(SaleModel sale)
		{
			Sale = sale;
		}

		public SaleModel Sale { get; set; }
	}

	public class CreateSaleCommandHandler: IRequestHandler<CreateSaleCommand, SaleModel>
	{
        private readonly IValidator<CreateSaleCommand> validator;
		private readonly ISaleAggregate saleAggregate;
		private readonly IUmvelChallengeDbContext context;

		public CreateSaleCommandHandler(
            IValidator<CreateSaleCommand> validator,
            ISaleAggregate saleAggregate,
            IUmvelChallengeDbContext context
			)
		{
			this.validator = validator;
			this.saleAggregate = saleAggregate;
			this.context = context;
		}

		public async Task<SaleModel> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
		{
			await validator.ValidateAndThrowAsync(request);

			using (context)
			{
				saleAggregate.AddSale(request.Sale);
				try
				{
					await context.Sales.AddAsync(saleAggregate.Sale);
					await context.SaveChangesAsync(cancellationToken);
				}
				catch (Exception ex)
				{
                    throw new ServiceException($"An error ocurred while inserting the entity", ex, ResponseCode.ServiceError);
                }

				return request.Sale;
			}
		}

		public class CreateSaleCommandValidator: AbstractValidator<CreateSaleCommand>
		{
			public CreateSaleCommandValidator()
			{
				RuleFor(p => p.Sale).NotNull().WithMessage("Sale must not be null");
				RuleFor(p => p.Sale.CustomerId).GreaterThan(0).WithMessage("CustomerId must be greater than 0");
				RuleFor(p => p.Sale.Date).NotNull().WithMessage("Date must not be null");
				RuleFor(p => p.Sale.Total).NotNull().NotEmpty().WithMessage("total must not be null");
			}
		}
    }
}

