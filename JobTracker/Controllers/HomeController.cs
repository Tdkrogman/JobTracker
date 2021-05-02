using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JobTracker.Models;
using Microsoft.AspNetCore.Http;

namespace JobTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly JobTrackerContext context;

        public HomeController(JobTrackerContext ctx)
        {
            context = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(Employee employee)
        {
            bool success = false;
            if(employee.Username == null)
            {
                employee.Username = "b";
            }
            if (employee.Password == null)
            {
                employee.Password = "b";
            }
            var employees = context.Employees.ToList();
            foreach(var e in employees)
            {
                if(employee.Username.ToLower().Equals(e.Username.ToLower()) && employee.Password.Equals(e.Password))
                {
                    employee = e;
                    success = true;
                }
            }

            if (success)
            {
                HttpContext.Session.SetString("currentUser", employee.FullName);
                HttpContext.Session.SetInt32("currentUserId", employee.EmployeeId);
                HttpContext.Session.SetString("currentRole", employee.Role);
                if (employee.Role == "admin")
                {
                    return Redirect("~/admin");
                }
                else
                {
                    return Redirect("~/main");
                }
            }
            else
            {
                ViewBag.Error = "Invalid Username/Password combination.";
                return View("Index");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString("currentUser", "");
            HttpContext.Session.SetInt32("currentUserId", 0);
            HttpContext.Session.SetString("currentRole", "");
            ViewBag.message = "You have been successfully logged out.";
            return View("Index");
        }
    }
}
