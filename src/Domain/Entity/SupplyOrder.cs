using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class SupplyOrder
    {
        public int Id { get; set; }
        public int DealerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SupplyOrderClientOrder> SupplyOrderClientOrders { get; set; } = new List<SupplyOrderClientOrder>();
    }
}
