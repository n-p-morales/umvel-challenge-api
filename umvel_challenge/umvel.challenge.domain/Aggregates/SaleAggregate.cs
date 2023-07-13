using System;
using umvel.challenge.domain.Commons.EntitiesBase;
using umvel.challenge.domain.Entities;
using umvel.challenge.domain.Entities.Sales.Rule;
using umvel.challenge.domain.Models.Sales;

namespace umvel.challenge.domain.Aggregates
{
	public class SaleAggregate: Aggregate, ISaleAggregate
	{
		private readonly ISaleIdMustExistRule saleIdMustExistRule;
		public SaleAggregate(ISaleIdMustExistRule saleIdMustExistRule)
		{
			this.saleIdMustExistRule = saleIdMustExistRule;
		}

		public Sale Sale { get; set; }

		public void AddSale(SaleModel sale)
		{
			saleIdMustExistRule.SaleId = sale.SaleId;
			ValidateRule(saleIdMustExistRule);
			Sale = Sale.Create(sale.SaleId, sale.Date, sale.CustomerId, sale.Total);
		}
	}
}

