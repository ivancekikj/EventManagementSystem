using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ITicketForPurchaseService
    {
        List<TicketForPurchase> GetAll();
        TicketForPurchase GetById(Guid? id);
        void Create(TicketForPurchase ticket);
        void Update(TicketForPurchase ticket);
        void DeleteById(Guid id);
    }
}
