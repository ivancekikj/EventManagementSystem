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
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ShoppingCart? GetByUserId(string userId)
        {
            return entities.Include(c => c.TicketInCarts)
                .Include("TicketInCarts.TicketForPurchase")
                .Include("TicketInCarts.TicketForPurchase.Schedule")
                .Include("TicketInCarts.TicketForPurchase.Schedule.Event")
                .FirstOrDefault(s => s.ApplicationUserId.Equals(userId));
        }
    }
}
