using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO;
using Domain.Entity;

namespace Application.Interfaces
{
    public interface ISendOrderUseCase
    {
        Task<ClientOrder> Execute(ClientOrderDTO orderDTO);
    }
}
