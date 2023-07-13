using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using umvel.challenge.application.Commands.Customer;
using umvel.challenge.application.Exceptions;
using umvel.challenge.domain.Aggregates;
using umvel.challenge.domain.Contexts;
using umvel.challenge.domain.Models.Products;

namespace umvel.challenge.application.Commands.Products
{
	public class CreateProductCommand: IRequest<ProductModel>
	{
		public CreateProductCommand(ProductModel product)
		{
			Product = product;
		}

		public ProductModel Product { get; set; }
	}

	public class CreateProductCommandHandler: IRequestHandler<CreateProductCommand, ProductModel>
	{
        private readonly IValidator<CreateProductCommand> validator;
		private readonly IProductAggregate productAggregate;
        private readonly IUmvelChallengeDbContext context;

		public CreateProductCommandHandler(
            IValidator<CreateProductCommand> validator,
            IProductAggregate productAggregate,
            IUmvelChallengeDbContext context
            )
		{
			this.validator = validator;
			this.productAggregate = productAggregate;
			this.context = context;
		}

		public async Task<ProductModel> Handle(CreateProductCommand request, CancellationToken cancellationToken)
		{
			await validator.ValidateAndThrowAsync(request);

			using (context)
			{
				productAggregate.AddProduct(request.Product);

				try
				{
					await context.Products.AddAsync(productAggregate.Product);
					await context.SaveChangesAsync(cancellationToken);
				}
				catch (Exception ex)
				{
					throw new ServiceException($"An error ocurred while inserting the entity", ex, ResponseCode.ServiceError);
				}

				return request.Product;
			}
		}
    }

	public class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
	{
		public CreateProductCommandValidator()
		{ 
            RuleFor(p => p.Product).NotNull().WithMessage("Product must not be null");
            RuleFor(p => p.Product.Name).NotNull().NotEmpty().WithMessage("Name must not be null or empty");
            RuleFor(p => p.Product.Cost).NotNull().WithMessage("Cost must not be null");
            RuleFor(p => p.Product.UnitPrice).NotNull().WithMessage("Unit priece must not be null");
        }
	}
}

