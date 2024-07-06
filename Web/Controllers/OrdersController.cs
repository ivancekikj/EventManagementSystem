using DocumentFormat.OpenXml.Office2010.Excel;
using Domain.Dto;
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
        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(_orderService.GetAllOrdersOwnedByUser(userId));
        }

        // GET: OrdersController/Details/5
        [Authorize(Roles = "User")]
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
            } catch (Exception ex)
            {
                return View("AccessDenied");
            }
            return View(dto);
        }

        [Authorize(Roles = "User")]
        public ActionResult ExportInvoice(Guid? purchasedTicketId)
        {
            if (purchasedTicketId == null)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            DocumentModel document;
            try
            {
                document = _orderService.CreateInvoice((Guid)purchasedTicketId, userId);
            } catch (Exception ex)
            {
                return View("AccessDenied");
            }
            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions());
            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "Invoice.pdf");
        }
    }
}
