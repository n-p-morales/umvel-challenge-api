using System;
using umvel.challenge.domain.Entities;
using umvel.challenge.domain.Entities.Customers;
using umvel.challenge.domain.Models.Customers;

namespace umvel.challenge.domain.Aggregates
{
	public interface ICustomerAggregate
	{
		Customer Customer { get; set; }

		void AddCustomer(CustomerModel customer);

		void UpdateCustomer(CustomerModel customer);
	}
}

