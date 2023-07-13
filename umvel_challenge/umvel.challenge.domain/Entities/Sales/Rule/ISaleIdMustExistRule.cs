using System;
using umvel.challenge.domain.Commons.Rules;

namespace umvel.challenge.domain.Entities.Sales.Rule
{
	public interface ISaleIdMustExistRule: IRule
	{
		public int SaleId { get; set; }
	}
}

