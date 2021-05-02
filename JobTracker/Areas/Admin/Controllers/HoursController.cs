using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobTracker.Models;
using JobTracker.ViewModels;
using Microsoft.AspNetCore.Http;

namespace JobTracker.Areas.Admin.Controllers
{
    public class HoursController : Controller
    {
        private readonly JobTrackerContext context;

        public HoursController(JobTrackerContext ctx)
        {
            context = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        public IActionResult GetEmployee()
        {
            ViewBag.Employees = context.Employees.ToList().Skip(1);
            return View();
        }

        public IActionResult List(int id, DateTime startDate)
        {
            Employee employee = context.Employees.Find(id);
            IEnumerable<WorkHour> workHours = context.WorkHours.Where(wh => wh.EmployeeId == employee.EmployeeId).Where(wh=>wh.Date.Month == startDate.Date.Month);
            EmployeeHoursViewModel vm = new EmployeeHoursViewModel
            {
                Employee = employee,
                StartDate = startDate,
                WorkHours = workHours
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            WorkHour workHour = context.WorkHours.Find(id);
            return View(workHour);
        }

        [HttpGet]
        public IActionResult Add(int id)
        {
            ViewBag.Action = "Add";
            WorkHour workHour = new WorkHour() { EmployeeId = id };
            return View("Edit", workHour);
        }

        [HttpPost]
        public IActionResult Edit(WorkHour workHour)
        {
            if (ModelState.IsValid)
            {
                if (workHour.WorkHourId == 0)
                {
                    context.WorkHours.Add(workHour);
                    TempData["successMessage"] = "Hours successfully added.";
                }
                else
                {
                    context.WorkHours.Update(workHour);
                    TempData["successMessage"] = "Hours successfully edited.";
                }
                context.SaveChanges();
                return RedirectToAction("List", new { id = workHour.EmployeeId, startDate = workHour.Date });
            }
            else
            {
                ViewBag.Action = workHour.WorkHourId == 0 ? "Add" : "Edit";
                return View(workHour);
            }
        }

        [HttpPost]
        public IActionResult Delete(WorkHour workHour)
        {
            context.Remove(workHour);
            context.SaveChanges();
            TempData["successMessage"] = "Hours successfully deleted.";
            return RedirectToAction("List", new { id = workHour.EmployeeId, startDate = workHour.Date });
        }

        public IActionResult PreviousMonth(int month, int year)
        {
            if (month == 1)
            {
                year = year - 1;
                month = 12;
            }
            else
            {
                month = month - 1;
            }
            int daysInMonth = DateTime.DaysInMonth(year, month);
            DateTime newDate = new DateTime(year, month, daysInMonth);

            return RedirectToAction("List", new { id = HttpContext.Session.GetInt32("currentUserId"), startDate = newDate });
        }

        public IActionResult NextMonth(int month, int year)
        {
            if (month == 12)
            {
                year = year + 1;
                month = 1;
            }
            else
            {
                month = month + 1;
            }
            int daysInMonth = DateTime.DaysInMonth(year, month);
            DateTime newDate = new DateTime(year, month, daysInMonth);
            int? userId = HttpContext.Session.GetInt32("currentUserId");

            return RedirectToAction("List", new { id = HttpContext.Session.GetInt32("currentUserId"), startDate = newDate });
        }
    }
}
