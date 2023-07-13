using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace umvel.challenge.domain.Entities
{
    public class Customer
    {
        private Customer() { }

        private Customer(int customerId, string name)
        {
            CustomerId = customerId;
            Name = name;
        }

        public int CustomerId { get; set; }

        public string Name { get; set; }

        public ICollection<Sale> Sales { get; set; }

        internal static Customer Create(int customerId, string name)
        {
            return new Customer(customerId, name);
        }
    }
}
