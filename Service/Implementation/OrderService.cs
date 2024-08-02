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

        public DocumentModel CreateInvoice(Guid orderId)
        {
            Order? order = _orderRepository.GetById(orderId);

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");
            var document = DocumentModel.Load(templatePath);

            document.Content.Replace("{{OrderId}}", order.Id.ToString());
            document.Content.Replace("{{Username}}", order.ApplicationUser.UserName);
            document.Content.Replace("{{DateTimeOrdered}}", order.DateTimeOrdered.ToString());
            float totalPrice = 0.0f;
            StringBuilder sb = new StringBuilder();
            PurchasedTicket pt;
            for (int i = 0; i < order.PurchasedTickets.Count; i++)
            {
                pt = order.PurchasedTickets.ElementAt(i);
                sb.Append($"Ticket with quantity {pt.Quantity}, price ${pt.Price} and discount {pt.Discount} for event '{pt.Schedule.Event.Name}' from {pt.Schedule.StartTime} to {pt.Schedule.EndTime}.");
                totalPrice += (float)((pt.Price * pt.Quantity) * (1 - pt.Discount));
                if (i < order.PurchasedTickets.Count - 1)
                    sb.Append("\n");
            }
            document.Content.Replace("{{PurchasedTickets}}", sb.ToString());
            document.Content.Replace("{{TotalPrice}}", "$" + totalPrice.ToString());

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
            ApplicationUser admin = _userManager.FindByIdAsync(userId).Result;
            if (!(_userManager.IsInRoleAsync(admin, "Admin").Result
                || order.ApplicationUserId.Equals(userId)))
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
