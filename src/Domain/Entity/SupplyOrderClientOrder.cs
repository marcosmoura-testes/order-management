using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class SupplyOrderClientOrder
    {
        public int Id { get; set; }
        public int SupplyOrderId { get; set; }
        public int ClientOrderId { get; set; }
    }
}
