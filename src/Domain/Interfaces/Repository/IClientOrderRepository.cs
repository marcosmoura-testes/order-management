﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace Domain.Interfaces.Repository
{
    public interface IClientOrderRepository : IBaseRepository<ClientOrder, int>
    {
        List<ClientOrder> GetAllByDealerIdStatusId(int dealerId, int statusId);
    }
}
