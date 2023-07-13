using System;
using Microsoft.Extensions.Configuration;
using umvel.challenge.domain.Commons.EntitiesBase;
using umvel.challenge.domain.Entities;
using umvel.challenge.domain.Entities.Customers;
using umvel.challenge.domain.Models.Customers;

namespace umvel.challenge.domain.Aggregates
{
	public class CustomerAggregate: Aggregate, ICustomerAggregate 
	{
		private readonly Entities.Customers.Rules.ICustomerIdMustExistRule customerIdMustExistRule;

		public CustomerAggregate(Entities.Customers.Rules.ICustomerIdMustExistRule customerIdMustExistRule)
		{
			this.customerIdMustExistRule = customerIdMustExistRule;
		}

		public Customer Customer { get; set; }

		public void AddCustomer(CustomerModel customer)
		{
			customerIdMustExistRule.CustomerId = customer.CustomerId;
			ValidateRule(customerIdMustExistRule);
			Customer = Customer.Create(customer.CustomerId, customer.Name);
		}

		public void UpdateCustomer(CustomerModel customer)
		{
			Customer = Customer.Create(customer.CustomerId, customer.Name);
		}
	}
}

