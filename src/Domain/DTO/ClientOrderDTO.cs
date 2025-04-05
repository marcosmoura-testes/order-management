using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class ClientOrderDTO
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ClientCNPJ { get; set; }
        public string DealerCNPJ { get; set; }
        public List<ClientOrderProductDTO> ClientOrderProducts { get; set; } = new List<ClientOrderProductDTO>();
    }
}
