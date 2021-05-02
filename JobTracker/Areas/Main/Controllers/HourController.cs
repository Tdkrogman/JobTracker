using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobTracker.Models;
using JobTracker.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace JobTracker.Areas.Main.Controllers
{
    public class HourController : Controller
    {
        private readonly JobTrackerContext context;

        public HourController(JobTrackerContext ctx)
        {
            context = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }
        public IActionResult ClockIn(DateTime startDate)
        {
            if(startDate.Year == 0001)
            {
                startDate = DateTime.Now;
            }
            Employee employee = context.Employees.Find(HttpContext.Session.GetInt32("currentUserId"));
            IEnumerable<WorkHour> workHours = context.WorkHours.Where(wh => wh.EmployeeId == employee.EmployeeId).Where(wh => wh.Date.Month == startDate.Date.Month);
            EmployeeHoursViewModel vm = new EmployeeHoursViewModel
            {
                Employee = employee,
                WorkHours = workHours,
                StartDate = startDate
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult RecordHours(WorkHour workHour)
        {
            workHour.Date = DateTime.Now;
            IEnumerable<WorkHour> empWorkedHours = context.WorkHours.Where(h => h.EmployeeId == workHour.EmployeeId).Where(h => h.Date.Date == DateTime.Now.Date);
            if (empWorkedHours.Count() > 0)
            {
                TempData["errorMessage"] = "You have already recorded hours for today.";
                return RedirectToAction("ClockIn");
            }
            else
            {
                context.WorkHours.Add(workHour);
                context.SaveChanges();
                TempData["successMessage"] = "Hours have been updated succefully.";
                return RedirectToAction("ClockIn");
            }
            
        }

        public IActionResult PreviousMonth(int month, int year)
        {
            if(month == 1)
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

            return RedirectToAction("ClockIn", new { startDate = newDate });
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

            return RedirectToAction("ClockIn", new { startDate = newDate });
        }
    }
}
