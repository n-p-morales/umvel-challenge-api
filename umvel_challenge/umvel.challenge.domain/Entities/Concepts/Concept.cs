using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umvel.challenge.domain.Entities;

namespace umvel.challenge.domain.Entities.Concepts
{
    public class Concept
    {
        public int ConceptId { get; set; }

        public decimal Quantity { get; set; }

        public int ProductId { get; set; }

        public int SaleId { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Amount { get; set; }

        public Sale Sale { get; set; }

        public Product Product { get; set; }
    }
}
