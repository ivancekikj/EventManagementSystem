﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set;  }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<TicketForPurchase> TicketForPurchases { get; set; }
        public DbSet<PurchasedTicket> Purchasedtickets { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<TicketInCart> TicketInCarts { get; set; }
    }
}
