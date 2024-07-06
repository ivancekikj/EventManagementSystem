using DocumentFormat.OpenXml.Office2010.Excel;
using Domain.Dto;
using Domain.Models;
using GemBox.Document;
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

        public OrderService(IOrderRepository orderRepository, 
            IPurchasedTicketRepository purchasedTicketRepository)
        {
            _orderRepository = orderRepository;
            _purchasedTicketRepository = purchasedTicketRepository;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        public DocumentModel CreateInvoice(Guid purchasedTicketId, string userId)
        {
            PurchasedTicket? ticket = _purchasedTicketRepository.GetWithScheduleUserAndEvent(purchasedTicketId);
            if (!ticket.Order.ApplicationUserId.Equals(userId))
            {
                throw new Exception($"Ticket {purchasedTicketId} doesn't belong to user {userId}");
            }

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

        public List<Order> GetAllOrdersOwnedByUser(string userId)
        {
            return _orderRepository.GetAllByUserId(userId);
        }

        public OrderDto GetOrderById(Guid id, string userId)
        {
            Order? order = _orderRepository.GetById(id);
            if (!order.ApplicationUserId.Equals(userId))
            {
                throw new Exception($"Order {id} doesn't belong to user {userId}");
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
