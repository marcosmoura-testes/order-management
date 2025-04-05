using System;
using System.Collections.Generic;

namespace Domain.Entity
{
    public class ClientOrder
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ClientCNPJ { get; set; }
        public int DealerId { get; set; }
        public int StatusId { get; set; }
        public decimal TotalAmount { get; set; }
        public List<ClientOrderProduct> CLientOrderProducts { get; set; } = new List<ClientOrderProduct>();
    }
}
