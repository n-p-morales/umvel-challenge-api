using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using umvel.challenge.application.Exceptions;
using umvel.challenge.domain.Aggregates;
using umvel.challenge.domain.Contexts;
using umvel.challenge.domain.Entities;
using umvel.challenge.domain.Models.Products;

namespace umvel.challenge.application.Commands.Products
{
	public class UpdateProductCommand: IRequest<ProductModel>
	{
		public UpdateProductCommand(ProductModel product)
		{
			Product = product;
		}

		public ProductModel Product { get; set; }
	}

	public class UpdateProductCommandHandler: IRequestHandler<UpdateProductCommand, ProductModel>
	{
		private readonly IValidator<UpdateProductCommand> validator;
		private readonly IProductAggregate productAggregate;
        private readonly IUmvelChallengeDbContext context;

		public UpdateProductCommandHandler(
            IValidator<UpdateProductCommand> validator,
            IProductAggregate productAggregate,
            IUmvelChallengeDbContext context
            )
		{
			this.validator = validator;
			this.productAggregate = productAggregate;
			this.context = context;
		}

		public async Task<ProductModel> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
		{
			await validator.ValidateAndThrowAsync(request);

			using (context)
			{
				if (DidTheProductChange(request))
				{
					productAggregate.UpdateProduct(request.Product);

					try
					{
						context.Products.Update(productAggregate.Product);
						context.SaveChanges();
					}
					catch (Exception ex)
					{
						throw new ServiceException($"An error ocurred while updating the entity", ex, ResponseCode.ServiceError);
					}
				}
			}

			return request.Product;
		}

		private bool DidTheProductChange(UpdateProductCommand request)
		{
			Product currentProduct = context.Products
				.AsNoTracking()
				.SingleOrDefault(p => p.ProductId == request.Product.ProductId);

			if(currentProduct != null && (currentProduct.Name != request.Product.Name || currentProduct.Cost != request.Product.Cost
				|| currentProduct.UnitPrice != request.Product.UnitPrice))
			{
				return true;
			}

			return false;
		}
    }

	public class UpdateProductCommandValidator: AbstractValidator<UpdateProductCommand>
	{
		public UpdateProductCommandValidator()
		{
			RuleFor(p => p.Product).NotNull().WithMessage("Product must not be null");
			RuleFor(p => p.Product.ProductId).GreaterThan(0).WithMessage("ProductId must be greater than 0");
			RuleFor(p => p.Product.Name).NotNull().NotEmpty().WithMessage("Name must not be null or empty");
			RuleFor(p => p.Product.Cost).NotNull().WithMessage("Cost must not be null");
			RuleFor(p => p.Product.UnitPrice).NotNull().WithMessage("Unit priece must not be null");
        }
	}
}

