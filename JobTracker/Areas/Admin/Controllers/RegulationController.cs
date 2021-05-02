using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobTracker.Models;
using JobTracker.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

namespace JobTracker.Areas.Admin.Controllers
{
    public class RegulationController : Controller
    {
        private readonly JobTrackerContext context;
        public RegulationController(JobTrackerContext ctx)
        {
            context = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }
        private RegulationEditViewModel GetEditViewModel(Regulation j, string action) =>
        new RegulationEditViewModel
        {
            Regulation = j,
            RegulationRequirements = context.RegulationRequirements.ToList(),
            Action = action
        };

        public IActionResult List()
        {
            IEnumerable<Regulation> regulations = context.Regulations.ToList();
            return View(regulations);
        }

        public IActionResult View(int id)
        {
            var r = context.RegulationRequirements.Where(r => r.RegulationId == id).ToList();
            Regulation regulation = context.Regulations.Where(r => r.RegulationId == id).FirstOrDefault();
            RegulationListViewModel vm = new RegulationListViewModel { Regulation = regulation,RegulationRequirements = r };
            return View(vm);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var vm = GetEditViewModel(new Regulation(), "Add");
            return View("Edit", vm);
        }

        [HttpGet]
        public IActionResult AddRequirement(int id)
        {
            ViewBag.Action = "Add";
            return View(new RegulationRequirement() {RegulationId = id});
        }

        [HttpGet]
        public IActionResult EditRequirement(int id)
        {
            ViewBag.Action = "Edit";
            var requirement = context.RegulationRequirements.Find(id);
            return View("AddRequirement", requirement);
        }

        [HttpPost]
        public IActionResult EditRequirement(RegulationRequirement regulationRequirement)
        {
            string action = regulationRequirement.RegulationRequirementId == 0 ? "added" : "edited";

            if (ModelState.IsValid)
            {
                if (regulationRequirement.RegulationRequirementId == 0)
                    context.RegulationRequirements.Add(regulationRequirement);
                else
                    context.RegulationRequirements.Update(regulationRequirement);
                context.SaveChanges();
                TempData["successMessage"] = "Regulation " + action + " successfully";
                return RedirectToAction("View", new {id = regulationRequirement.RegulationId });
            }
            else
            {
                ViewBag.Action = regulationRequirement.RegulationRequirementId == 0 ? "Add" : "Edit";
                return View("AddRequirement", regulationRequirement);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var regulation = context.Regulations.Find(id);
            var vm = GetEditViewModel(regulation, "Edit");
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(Regulation regulation)
        {
            if (ModelState.IsValid)
            {
                if (regulation.RegulationId == 0)
                    context.Regulations.Add(regulation);
                else
                    context.Regulations.Update(regulation);
                context.SaveChanges();
                TempData["successMessage"] = "Requirement Added successfully";
                return RedirectToAction("List");
            }
            else
            {
                var action = regulation.RegulationId == 0 ? "Add" : "Edit";
                var vm = GetEditViewModel(regulation, action);
                return View(vm);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var j = context.Regulations.FirstOrDefault(j => j.RegulationId == id);
            return View(j);
        }

        [HttpPost]
        public IActionResult Delete(Regulation regulation)
        {
            context.Regulations.Remove(regulation);
            context.SaveChanges();
            TempData["successMessage"] = "Regulation deleted successfully";
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult DeleteRequirement(int id)
        {
            var j = context.RegulationRequirements.FirstOrDefault(j => j.RegulationRequirementId == id);
            return View(j);
        }

        [HttpPost]
        public IActionResult DeleteRequirement(RegulationRequirement regulationRequirement)
        {
            context.RegulationRequirements.Remove(regulationRequirement);
            context.SaveChanges();
            TempData["successMessage"] = "Requirement deleted successfully";
            return RedirectToAction("View", new {id = regulationRequirement.RegulationId });
        }
    }
}
