using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    /// <summary>
    /// Represents the relationship between a supply order and a client order.
    /// </summary>
    public class SupplyOrderClientOrder
    {
        /// <summary>
        /// The unique identifier for the SupplyOrderClientOrder.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The unique identifier for the SupplyOrder.
        /// </summary>
        public int SupplyOrderId { get; set; }

        /// <summary>
        /// The unique identifier for the ClientOrder.
        /// </summary>
        public int ClientOrderId { get; set; }
    }
}
