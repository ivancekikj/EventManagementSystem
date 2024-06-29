using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    public class TicketForPurchasesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketForPurchasesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TicketForPurchases
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TicketForPurchases.Include(t => t.Schedule);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TicketForPurchases/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketForPurchase = await _context.TicketForPurchases
                .Include(t => t.Schedule)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "Id");
            return View();
        }

        // POST: TicketForPurchases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Price,Discount,ScheduleId")] TicketForPurchase ticketForPurchase)
        {
            if (ModelState.IsValid)
            {
                ticketForPurchase.Id = Guid.NewGuid();
                _context.Add(ticketForPurchase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "Id", ticketForPurchase.ScheduleId);
            return View(ticketForPurchase);
        }

        // GET: TicketForPurchases/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketForPurchase = await _context.TicketForPurchases.FindAsync(id);
            if (ticketForPurchase == null)
            {
                return NotFound();
            }
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "Id", ticketForPurchase.ScheduleId);
            return View(ticketForPurchase);
        }

        // POST: TicketForPurchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Price,Discount,ScheduleId")] TicketForPurchase ticketForPurchase)
        {
            if (id != ticketForPurchase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketForPurchase);
                    await _context.SaveChangesAsync();
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
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "Id", ticketForPurchase.ScheduleId);
            return View(ticketForPurchase);
        }

        // GET: TicketForPurchases/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketForPurchase = await _context.TicketForPurchases
                .Include(t => t.Schedule)
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ticketForPurchase = await _context.TicketForPurchases.FindAsync(id);
            if (ticketForPurchase != null)
            {
                _context.TicketForPurchases.Remove(ticketForPurchase);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketForPurchaseExists(Guid id)
        {
            return _context.TicketForPurchases.Any(e => e.Id == id);
        }
    }
}
