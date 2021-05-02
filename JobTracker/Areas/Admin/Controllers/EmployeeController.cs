using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobTracker.Models;
using JobTracker.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Areas.Admin.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly JobTrackerContext context;

        public EmployeeController(JobTrackerContext ctx)
        {
            context = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        [HttpGet]
        public IActionResult List()
        {
            IEnumerable<Employee> employees = context.Employees.Skip(1).ToList();
            
            return View(employees);
        }
        
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Add", new Employee());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            Employee employee = context.Employees.Find(id);
            return View("Add", employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                string action = "edited";

                if (employee.EmployeeId == 0)
                {
                    context.Employees.Add(employee);
                    action = "added";
                }
                else
                    context.Employees.Update(employee);

                context.SaveChanges();
                TempData["successMessage"] = "Employee " + action + " successfully.";
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.Action = employee.EmployeeId == 0 ? "Add" : "Edit";
                return View("Add", employee);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var j = context.Employees.Find(id);
            return View(j);
        }

        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            context.Employees.Remove(employee);
            context.SaveChanges();
            TempData["successMessage"] = "Employee deleted successfully.";
            return RedirectToAction("List");
        }       
    }
}
