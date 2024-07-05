using Domain.Dto;
using Domain.Models;
using Repository.Implementation;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IPurchasedTicketRepository _purchasedTicketRepository;

        public ShoppingCartService(IShoppingCartRepository cartRepository, IOrderRepository orderRepository, IPurchasedTicketRepository purchasedTicketRepository)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _purchasedTicketRepository = purchasedTicketRepository;
        }

        public bool AddToShoppingConfirmed(TicketInCart model, string userId)
        {
            var userShoppingCart = _cartRepository.GetByUserId(userId);

            if (userShoppingCart.TicketInCarts == null)
                userShoppingCart.TicketInCarts = new List<TicketInCart>(); ;

            userShoppingCart.TicketInCarts.Add(model);
            _cartRepository.Update(userShoppingCart);
            return true;
        }

        public bool DeleteTicketFromShoppingCart(string userId, Guid ticketId)
        {
            if (ticketId != null)
            {
                var userShoppingCart = _cartRepository.GetByUserId(userId);
                var ticket = userShoppingCart.TicketInCarts.Where(x => x.TicketForPurchaseId == ticketId).FirstOrDefault();

                userShoppingCart.TicketInCarts.Remove(ticket);

                _cartRepository.Update(userShoppingCart);
                return true;
            }
            return false;
        }

        public ShoppingCartDto GetShoppingCartInfo(string userId)
        {
            var userShoppingCart = _cartRepository.GetByUserId(userId);
            var allProduct = userShoppingCart?.TicketInCarts?.ToList();

            var totalPrice = allProduct.Select(x => (x.TicketForPurchase.Price * x.Quantity) * (1 - x.TicketForPurchase.Discount)).Sum();

            ShoppingCartDto dto = new ShoppingCartDto
            {
                Products = allProduct,
                TotalPrice = (float)totalPrice
            };
            return dto;
        }

        public bool Order(string userId)
        {
            if (userId != null)
            {
                var userShoppingCart = _cartRepository.GetByUserId(userId);

                if (userShoppingCart.TicketInCarts.Count == 0)
                    return false;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = userId,
                    DateTimeOrdered = DateTime.Now
                };

                _orderRepository.Insert(order);

                userShoppingCart.TicketInCarts
                    .Select(x => new PurchasedTicket
                    {
                        Id = Guid.NewGuid(),
                        OrderId = order.Id,
                        Quantity = x.Quantity,
                        Price = x.TicketForPurchase.Price,
                        Discount = x.TicketForPurchase.Discount
                    })
                    .ToList()
                    .ForEach(t => _purchasedTicketRepository.Insert(t));

                userShoppingCart.TicketInCarts.Clear();
                _cartRepository.Update(userShoppingCart);

                return true;
            }
            return false;
        }
    }
}
