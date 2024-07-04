using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class TicketForPurchaseRepository : Repository<TicketForPurchase>, ITicketForPurchaseRepository
    {
        public TicketForPurchaseRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<TicketForPurchase> GetAllWithSchedule()
        {
            return entities.Include(t => t.Schedule);
        }

        public TicketForPurchase GetWithSchedule(Guid? id)
        {
            return entities.Include(t => t.Schedule)
                .SingleOrDefault(t => t.Id == id); ;
        }
    }
}
