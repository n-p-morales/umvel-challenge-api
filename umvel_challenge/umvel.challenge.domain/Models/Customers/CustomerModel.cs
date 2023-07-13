using System;
using umvel.challenge.domain.Entities;
namespace umvel.challenge.domain.Models.Customers
{
	public class CustomerModel
	{
		public CustomerModel() { }
		public CustomerModel(Customer customers)
		{
			CustomerId = customers.CustomerId;
			Name = customers.Name;
		}

        public int CustomerId { get; set; }
        public string Name { get; set; }
    }
}

