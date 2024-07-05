using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Service.Implementation;
using Service.Interface;


namespace Web.Controllers
{
    public class TicketForPurchasesController : Controller
    {
        private readonly ITicketForPurchaseService _ticketPurchaseService;
        private readonly IScheduleService _scheduleService;
        private readonly IShoppingCartService _shoppingCartService;

        public TicketForPurchasesController(ITicketForPurchaseService ticketPurchaseService, 
            IScheduleService scheduleService, 
            IShoppingCartService shoppingCartService)
        {
            _ticketPurchaseService = ticketPurchaseService;
            _scheduleService = scheduleService;
            _shoppingCartService = shoppingCartService;
        }

        [Authorize(Roles = "User")]
        public IActionResult AddToCart(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = _ticketPurchaseService.GetById(id);

            TicketInCart tic = new TicketInCart();

            if (ticket != null)
            {
                tic.TicketForPurchaseId = ticket.Id;
            }

            return View(tic);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult AddToCartConfirmed(TicketInCart model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _shoppingCartService.AddToShoppingConfirmed(model, userId);
            return RedirectToAction("Index");
        }

        // GET: TicketForPurchases
        public IActionResult Index()
        {
            return View(_ticketPurchaseService.GetAll());
        }

        // GET: TicketForPurchases/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketForPurchase = _ticketPurchaseService.GetById(id);
            if (ticketForPurchase == null)
            {
                return NotFound();
            }

            return View(ticketForPurchase);
        }

        // GET: TicketForPurchases/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["ScheduleId"] = new SelectList(_scheduleService.GetAll(), "Id", "Id");
            return View();
        }

        // POST: TicketForPurchases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Price,Discount,ScheduleId")] TicketForPurchase ticketForPurchase)
        {
            if (ModelState.IsValid)
            {
                _ticketPurchaseService.Create(ticketForPurchase);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ScheduleId"] = new SelectList(_scheduleService.GetAll(), "Id", "Id", ticketForPurchase.ScheduleId);
            return View(ticketForPurchase);
        }

        // GET: TicketForPurchases/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketForPurchase = _ticketPurchaseService.GetById(id);
            if (ticketForPurchase == null)
            {
                return NotFound();
            }
            ViewData["ScheduleId"] = new SelectList(_scheduleService.GetAll(), "Id", "Id", ticketForPurchase.ScheduleId);
            return View(ticketForPurchase);
        }

        // POST: TicketForPurchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Price,Discount,ScheduleId")] TicketForPurchase ticketForPurchase)
        {
            if (id != ticketForPurchase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _ticketPurchaseService.Update(ticketForPurchase);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketForPurchaseExists(ticketForPurchase.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ScheduleId"] = new SelectList(_scheduleService.GetAll(), "Id", "Id", ticketForPurchase.ScheduleId);
            return View(ticketForPurchase);
        }

        // GET: TicketForPurchases/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketForPurchase = _ticketPurchaseService.GetById(id);
            if (ticketForPurchase == null)
            {
                return NotFound();
            }

            return View(ticketForPurchase);
        }

        // POST: TicketForPurchases/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _ticketPurchaseService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TicketForPurchaseExists(Guid id)
        {
            return _ticketPurchaseService.GetById(id) != null;
        }
    }
}
