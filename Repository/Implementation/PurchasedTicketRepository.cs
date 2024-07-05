using Domain.Models;
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
    }
}
