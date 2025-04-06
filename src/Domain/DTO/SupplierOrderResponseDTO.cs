using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class SupplierOrderResponseDTO
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SupplierOrderItensDTO> SupplierOrderItens { get; set; }
    }
}
