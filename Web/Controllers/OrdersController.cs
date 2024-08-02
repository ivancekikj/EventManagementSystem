using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Dto;
using Domain.Exceptions;
using Domain.Models;
using GemBox.Document;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Interface;
using System.Security.Claims;
using System.Text;

namespace Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: OrdersController
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Order> orders = User.IsInRole("User")
                ? _orderService.GetAllOrdersOwnedByUser(userId)
                : _orderService.GetAll();
            return View(orders);
        }

        // GET: OrdersController/Details/5
        [Authorize]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            OrderDto dto;
            try
            {
                dto = _orderService.GetOrderById((Guid)id, userId);
            } catch (AccessDeniedException ex)
            {
                return View("AccessDenied");
            }
            return View(dto);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ExportInvoice(Guid? orderId)
        {
            if (orderId == null)
            {
                return NotFound();
            }
            DocumentModel document = _orderService.CreateInvoice((Guid)orderId);
            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions());
            return File(stream.ToArray(), new PdfSaveOptions().ContentType, $"Order_{orderId}_Invoice.pdf");
        }
    }
}
