using JobTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobTracker.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        private readonly JobTrackerContext context;
        public CustomerController(JobTrackerContext ctx)
        {
            context = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        public IActionResult List()
        {
            IEnumerable<Customer> customers = context.Customers.ToList();
            return View(customers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Customer());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var customer = context.Customers.Find(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            string action = customer.CustomerId == 0 ? "added" : "edited";

            if (ModelState.IsValid)
            {
                if (customer.CustomerId == 0)
                    context.Customers.Add(customer);
                else
                    context.Customers.Update(customer);
                context.SaveChanges();
                TempData["successMessage"] = "Customer " + action + " successfully";
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.Action = customer.CustomerId == 0 ? "Add" : "Edit";
                return View("Edit", customer);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var c = context.Customers.Find(id);
            return View(c);
        }
        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            context.Customers.Remove(customer);
            context.SaveChanges();
            TempData["successMessage"] = "Customer deleted successfully";
            return RedirectToAction("List");
        }
    }
}
