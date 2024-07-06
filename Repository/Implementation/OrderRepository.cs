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
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public List<Order> GetAllByUserId(string userId)
        {
            return entities.Include(o => o.ApplicationUser)
                .Include(o => o.PurchasedTickets)
                .Include("PurchasedTickets.Schedule")
                .Include("PurchasedTickets.Schedule.Event")
                .Where(o => o.ApplicationUserId.Equals(userId))
                .ToList();
        }

        public Order? GetById(Guid id)
        {
            return entities.Include(o => o.ApplicationUser)
                .Include(o => o.PurchasedTickets)
                .Include("PurchasedTickets.Schedule")
                .Include("PurchasedTickets.Schedule.Event")
                .FirstOrDefault(o => o.Id.Equals(id));
        }
    }
}
