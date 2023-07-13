using System;
using umvel.challenge.domain.Commons.Rules;

namespace umvel.challenge.domain.Entities.Products.Rules
{
	public interface IProductIdMustExistRule : IRule
	{
		public int ProductId { get; set; }
	}
}

