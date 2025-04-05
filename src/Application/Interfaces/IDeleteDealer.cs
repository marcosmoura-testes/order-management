using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDeleteDealer
    {
        Task<bool> Execute(int id);
    }
}
