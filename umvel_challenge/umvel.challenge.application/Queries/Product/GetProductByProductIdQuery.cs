using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using umvel.challenge.domain.Contexts;
using umvel.challenge.domain.Models.Products;

namespace umvel.challenge.application.Queries.Product
{
	public class GetProductByProductIdQuery: IRequest<ProductModel>
	{
		public GetProductByProductIdQuery(int productId)
		{
			ProductId = productId;
		}

		public int ProductId { get; set; }
	}

	public class GetProductByProductIdQueryHandler : IRequestHandler<GetProductByProductIdQuery, ProductModel>
	{
		private readonly IValidator<GetProductByProductIdQuery> validator;
		private readonly IUmvelChallengeDbContext context;

		public GetProductByProductIdQueryHandler(
            IValidator<GetProductByProductIdQuery> validator,
            IUmvelChallengeDbContext context
            )
		{
			this.validator = validator;
			this.context = context;
		}

		public async Task<ProductModel> Handle(GetProductByProductIdQuery request, CancellationToken cancellationToken)
		{
			validator.ValidateAndThrow(request);

			using (context)
			{
				var product = await context.Products
					.AsNoTracking()
					.Where(p => p.ProductId == request.ProductId)
					.Select(p => new ProductModel(p))
					.FirstOrDefaultAsync();

				return product ?? new ProductModel();
			}
		}
    }

	public class GetProductByProductIdQueryValidator : AbstractValidator<GetProductByProductIdQuery>
	{
		public GetProductByProductIdQueryValidator()
		{
			RuleFor(c => c.ProductId).GreaterThan(0).WithMessage("ProductId must be greater than 0");
		}
	}
}

