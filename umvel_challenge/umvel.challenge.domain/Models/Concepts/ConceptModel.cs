using System;
namespace umvel.challenge.domain.Models.Concepts
{
	public class ConceptModel
	{
		public ConceptModel()
		{
		}

        public int ConceptId { get; set; }

        public decimal Quantity { get; set; }

        public int ProductId { get; set; }

        public int SaleId { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Amount { get; set; }
    }
}

