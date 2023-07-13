using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umvel.challenge.domain.Entities.Concepts;

namespace umvel.challenge.domain.Entities
{
    public class Product
    {
        private Product() { }

        private Product(int productId, string name, string unitPrice, decimal cost)
        {
            ProductId = productId;
            Name = name;
            UnitPrice = unitPrice;
            Cost = cost;
        }

        public int ProductId { get; set; }

        public string Name { get; set; }

        public string UnitPrice { get; set; }

        public decimal Cost { get; set; }

        public Concept Concept { get; set; }

        internal static Product Create(int productId, string name, string unitPrice, decimal cost)
        {
            return new Product(productId, name, unitPrice, cost);
        }
    }
}
