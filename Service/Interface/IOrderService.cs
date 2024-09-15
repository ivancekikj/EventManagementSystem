using Domain.Dto;
using Domain.Models;
using GemBox.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IOrderService
    {
        List<Order> GetAll();
        List<Order> GetAllOrdersOwnedByUser(string userId);
        OrderDto GetOrderById(Guid id, string userId);
        DocumentModel CreateInvoice(Guid orderId, string contentRootPath);
    }
}
