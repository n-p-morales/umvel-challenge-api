using System;
using umvel.challenge.domain.Entities;
using umvel.challenge.domain.Models.Products;

namespace umvel.challenge.domain.Aggregates
{
	public interface IProductAggregate
	{
		Product Product { get; set; }

		void AddProduct(ProductModel product);

		void UpdateProduct(ProductModel product);
	}
}

