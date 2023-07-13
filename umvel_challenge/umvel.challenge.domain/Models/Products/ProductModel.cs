using System;
using umvel.challenge.domain.Entities;

namespace umvel.challenge.domain.Models.Products
{
	public class ProductModel
	{
        public ProductModel() { }

		public ProductModel(Product product)
		{
            ProductId = product.ProductId;
            Name = product.Name;
            UnitPrice = product.UnitPrice;
            Cost = product.Cost;
		}

        public int ProductId { get; set; }

        public string Name { get; set; }

        public string UnitPrice { get; set; }

        public decimal Cost { get; set; }
    }
}

