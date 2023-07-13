using System;
using umvel.challenge.domain.Entities;
using umvel.challenge.domain.Models.Sales;

namespace umvel.challenge.domain.Aggregates
{
	public interface ISaleAggregate
	{
		Sale Sale { get; set; }

		void AddSale(SaleModel sale);
	}
}

