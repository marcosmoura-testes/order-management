using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Interfaces.Repository;

namespace Infrastructure.Repository
{
    public class SupplyOrderRepository : BaseRepository<SupplyOrder, int>, ISupplyOrderRepository
    {
        private readonly DefaultDbContext _context;

        public SupplyOrderRepository(DefaultDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
