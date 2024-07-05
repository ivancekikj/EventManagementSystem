using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System.Security.Claims;

namespace Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }


        // GET: ShoppingCartController
        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dto = _shoppingCartService.GetShoppingCartInfo(userId);
            return View(dto);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult DeleteFromShoppingCart(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _shoppingCartService.DeleteTicketFromShoppingCart(userId, id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _shoppingCartService.Order(userId);
            return RedirectToAction("Index");
        }
    }
}
