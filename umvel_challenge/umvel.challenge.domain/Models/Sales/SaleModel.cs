using System;
using umvel.challenge.domain.Entities;

namespace umvel.challenge.domain.Models.Sales
{
	public class SaleModel
	{
		public SaleModel()
		{
		}

        public SaleModel(Sale sale)
        {
            SaleId = sale.SaleId;
            Date = sale.Date;
            CustomerId = sale.CustomerId;
            Total = sale.Total;
        }

        public int SaleId { get; set; }

        public DateTime Date { get; set; }

        public int CustomerId { get; set; }

        public decimal Total { get; set; }
    }
}

