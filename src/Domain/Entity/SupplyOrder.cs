using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    /// <summary>
    /// Represents a supply order.
    /// </summary>
    public class SupplyOrder
    {
        /// <summary>
        /// The unique identifier for the supply order.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The unique identifier for the dealer.
        /// </summary>
        public int DealerId { get; set; }

        /// <summary>
        /// The unique identifier for the parent supply order, if any.
        /// </summary>
        public int? SupplyOrderId { get; set; }

        /// <summary>
        /// The date and time when the supply order was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The total amount of the supply order.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// The status identifier of the supply order.
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// The list of client orders associated with the supply order.
        /// </summary>
        public List<SupplyOrderClientOrder> SupplyOrderClientOrders { get; set; } = new List<SupplyOrderClientOrder>();
    }
}
