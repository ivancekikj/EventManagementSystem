using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Service.Interface;

namespace Web.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly IScheduleService _scheduleService;
        private readonly IEventService _eventService;

        public SchedulesController(IScheduleService scheduleService, IEventService eventService)
        {
            _scheduleService = scheduleService;
            _eventService = eventService;
        }


        // GET: Schedules
        public IActionResult Index()
        {
            return View(_scheduleService.GetAll());
        }

        // GET: Schedules/Details/5

        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = _scheduleService.GetById(id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Schedules/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_eventService.GetAll(), "Id", "Description");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,StartTime,EndTime,EventId")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                _scheduleService.Create(schedule);
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_eventService.GetAll(), "Id", "Description", schedule.EventId);
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = _scheduleService.GetById(id);
            if (schedule == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_eventService.GetAll(), "Id", "Description", schedule.EventId);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,StartTime,EndTime,EventId")] Schedule schedule)
        {
            if (id != schedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _scheduleService.Update(schedule);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.Id))
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
            ViewData["EventId"] = new SelectList(_eventService.GetAll(), "Id", "Description", schedule.EventId);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = _scheduleService.GetById(id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _scheduleService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(Guid id)
        {
            return _scheduleService.GetById(id) != null;
        }
    }
}
