using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.Interfaces
{
    public interface IUpdateDealer
    {
        Task<Dealer> Execute(Dealer dealer, int dealerId);
    }
}
