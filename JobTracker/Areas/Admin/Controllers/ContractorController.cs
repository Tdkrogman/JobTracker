using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobTracker.Models;

namespace JobTracker.Areas.Admin.Controllers
{
    public class ContractorController : Controller
    {
        private readonly JobTrackerContext context;

        public ContractorController(JobTrackerContext ctx)
        {
            context = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        [HttpGet]
        public IActionResult List()
        {
            IEnumerable<Contractor> contractors = context.Contractors.Skip(1).ToList();

            return View(contractors);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Add", new Contractor());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            Contractor contractor = context.Contractors.Find(id);
            return View("Add", contractor);
        }

        [HttpPost]
        public IActionResult Edit(Contractor contractor)
        {
            if (ModelState.IsValid)
            {
                string action = "edited";

                if (contractor.ContractorId == 0)
                {
                    context.Contractors.Add(contractor);
                    action = "added";
                }
                else
                    context.Contractors.Update(contractor);

                context.SaveChanges();
                TempData["successMessage"] = "Contractor " + action + " successfully.";
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.Action = contractor.ContractorId == 0 ? "Add" : "Edit";
                return View("Add", contractor);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Contractor c = context.Contractors.Find(id);
            return View(c);
        }

        [HttpPost]
        public IActionResult Delete(Contractor contractor)
        {
            context.Contractors.Remove(contractor);
            context.SaveChanges();
            TempData["successMessage"] = "Contractor deleted successfully.";
            return RedirectToAction("List");
        }
    }
}
