using System;
using umvel.challenge.domain.Commons.EntitiesBase;
using umvel.challenge.domain.Entities;
using umvel.challenge.domain.Entities.Products.Rules;
using umvel.challenge.domain.Models.Products;

namespace umvel.challenge.domain.Aggregates
{
	public class ProductAggregate: Aggregate, IProductAggregate
	{
		private readonly IProductIdMustExistRule productIdMustExistRule;
		public ProductAggregate(IProductIdMustExistRule productIdMustExistRule)
		{
			this.productIdMustExistRule = productIdMustExistRule;
		}

		public Product Product { get; set; }

		public void AddProduct(ProductModel product)
		{
			productIdMustExistRule.ProductId = product.ProductId;
			ValidateRule(productIdMustExistRule);
			Product = Product.Create(product.ProductId, product.Name, product.UnitPrice, product.Cost);
		}

		public void UpdateProduct(ProductModel product)
		{
			Product = Product.Create(product.ProductId, product.Name, product.UnitPrice, product.Cost);
		}
	}
}

