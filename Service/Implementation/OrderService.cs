using DocumentFormat.OpenXml.Office2010.Excel;
using Domain.Dto;
using Domain.Exceptions;
using Domain.Models;
using GemBox.Document;
using Microsoft.AspNetCore.Identity;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPurchasedTicketRepository _purchasedTicketRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderService(IOrderRepository orderRepository, 
            IPurchasedTicketRepository purchasedTicketRepository, 
            UserManager<ApplicationUser> userManager)
        {
            _orderRepository = orderRepository;
            _purchasedTicketRepository = purchasedTicketRepository;
            _userManager = userManager;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        public DocumentModel CreateInvoice(Guid purchasedTicketId)
        {
            PurchasedTicket? ticket = _purchasedTicketRepository.GetWithScheduleUserAndEvent(purchasedTicketId);

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");
            var document = DocumentModel.Load(templatePath);

            document.Content.Replace("{{TicketId}}", ticket.Id.ToString());
            document.Content.Replace("{{Username}}", ticket.Order.ApplicationUser.UserName);
            document.Content.Replace("{{OrderDateTime}}", ticket.Order.DateTimeOrdered.ToString());
            document.Content.Replace("{{Quantity}}", ticket.Quantity.ToString());
            document.Content.Replace("{{Price}}", "$" + ticket.Price.ToString());
            document.Content.Replace("{{Discount}}", ticket.Discount.ToString());
            float? value = (1 - ticket.Discount) * (ticket.Price * ticket.Quantity);
            document.Content.Replace("{{TotalPrice}}", "$" + value.ToString());

            return document;
        }

        public List<Order> GetAll()
        {
            return _orderRepository.GetAllWithExtraData();
        }

        public List<Order> GetAllOrdersOwnedByUser(string userId)
        {
            return _orderRepository.GetAllByUserId(userId);
        }

        public OrderDto GetOrderById(Guid id, string userId)
        {
            Order? order = _orderRepository.GetById(id);
            if (!(_userManager.IsInRoleAsync(order.ApplicationUser, "Admin").Result
                || _userManager.IsInRoleAsync(order.ApplicationUser,"User").Result 
                && order.ApplicationUserId.Equals(userId)))
            {
                throw new AccessDeniedException();
            }
            float? totalPrice = order.PurchasedTickets
                .Select(x => (x.Price * x.Quantity) * (1 - x.Discount))
                .Sum();
            return new()
            {
                Order = order,
                TotalPrice = (float)totalPrice
            };
        }
    }
}
