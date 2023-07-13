using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umvel.challenge.domain.Entities.Concepts;

namespace umvel.challenge.domain.Entities
{
    public class Sale
    {
        private Sale() { }

        private Sale(int saleId, DateTime date, int customerId, decimal total)
        {
            SaleId = saleId;
            Date = date;
            CustomerId = customerId;
            Total = total;
        }

        public int SaleId { get; set; }

        public DateTime Date { get; set; }

        public int CustomerId { get; set; }

        public decimal Total { get; set; }

        public Customer Customer { get; set; }

        public ICollection<Concept> Concepts { get; set; }

        internal static Sale Create(int saleId, DateTime date, int customerId, decimal total)
        {
            return new Sale(saleId, date, customerId, total);
        }
    }
}
