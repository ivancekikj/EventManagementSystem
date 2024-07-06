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
    public class PurchasedTicketRepository : Repository<PurchasedTicket>, IPurchasedTicketRepository
    {
        public PurchasedTicketRepository(ApplicationDbContext context) : base(context)
        {
        }

        public PurchasedTicket? GetWithScheduleUserAndEvent(Guid id)
        {
            return entities.Include(pt => pt.Schedule)
                .Include("Schedule.Event")
                .Include(pt => pt.Order)
                .Include("Order.ApplicationUser")
                .FirstOrDefault(pt => pt.Id.Equals(id));
        }
    }
}
