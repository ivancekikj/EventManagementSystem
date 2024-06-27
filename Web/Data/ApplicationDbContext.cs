using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Event> Events { get; set;  }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<TicketForPurchase> TicketForPurchases { get; set; }
        public DbSet<PurchasedTicket> Purchasedtickets { get; set; }
    
    }
}
