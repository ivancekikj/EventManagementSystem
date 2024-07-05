using Domain.Dto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto GetShoppingCartInfo(string userId);
        bool DeleteTicketFromShoppingCart(string userId, Guid ticketId);
        bool Order(string userId);
        bool AddToShoppingConfirmed(TicketInCart model, string userId);
    }
}
