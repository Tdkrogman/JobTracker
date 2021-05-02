using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobTracker.Models;
using JobTracker.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace JobTracker.Areas.Admin.Controllers
{
    public class JobController : Controller
    {
        private readonly JobTrackerContext context;

        public JobController(JobTrackerContext ctx)
        {
            context = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        private JobEditViewModel GetEditViewModel(Job j, string action) =>
            new JobEditViewModel
            {
                Job = j,
                Customers = context.Customers.ToList(),
                Action = action
            };

        [HttpGet]
        public IActionResult List(string filter = "all")
        {
            IQueryable<Job> query = context.Jobs.Include(j => j.Customer);

            if(filter == "open")
            {
                query = query.Where(j => j.Status == "Open");
            }
            if (filter == "waitingOnCustomer")
            {
                query = query.Where(j => j.Status == "Waiting on Customer");
            }
            if (filter == "inProgress")
            {
                query = query.Where(j => j.Status == "In Progress");
            }
            if (filter == "closed")
            {
                query = query.Where(j => j.Status == "Closed");
            }

            List<Job> jobs = query.OrderBy(j=>j.StartDate).ToList();

            JobListViewModel model = new JobListViewModel
            {
                Filter = filter,
                Jobs = jobs
            };

            return View(model);
        }
        
        [HttpGet]
        public IActionResult Add()
        {
            var vm = GetEditViewModel(new Job(), "Add");
            return View("Edit", vm);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var job = context.Jobs.Find(id);
            var vm = GetEditViewModel(job, "Edit");
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(Job job)
        {
            if (ModelState.IsValid)
            {
                string action = "edited";
                if (job.JobId == 0)
                {
                    context.Jobs.Add(job);
                    action = "added";
                }
                else
                    context.Jobs.Update(job);

                context.SaveChanges();
                TempData["successMessage"] = "Job "+ action + " successfully.";
                return RedirectToAction("View", new { id = job.JobId });
            }
            else
            {
                var action = job.JobId == 0 ? "Add" : "Edit";
                var vm = GetEditViewModel(job, action);
                return View(vm);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var j = context.Jobs.Include(j=>j.Customer).FirstOrDefault(j=>j.JobId == id);
            return View(j);
        }

        [HttpPost]
        public IActionResult Delete(Job job)
        {
            context.Jobs.Remove(job);
            context.SaveChanges();
            TempData["successMessage"] = "Job deleted successfully.";
            return RedirectToAction("List");
        }

        public IActionResult View(int id)
        {
            HttpContext.Session.SetInt32("currentJob", id);
            var job = context.Jobs.Include(j => j.Customer).FirstOrDefault(j => j.JobId == id);
            var tasks = context.Tasks.Where(t => t.JobId == id).Include(ta=>ta.TaskAssignments).OrderBy(t=>t.EstCompletionDate).ToList();
            var employees = context.Employees.ToList();
            var contractors = context.Contractors.ToList();
            var jobRegulations = context.JobRegulations.Include(jr => jr.Regulation).Include(jr => jr.Regulation.RegulationRequirements).Where(jr => jr.JobId == id).ToList();
            var vm = new JobViewModel
            {
                Job = job,
                Tasks = tasks,
                Employees = employees,
                Contractors = contractors,
                JobRegulations = jobRegulations
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult RemoveRegulation(int id)
        {
            var jr = context.JobRegulations.Include(jr => jr.Regulation).FirstOrDefault(j => j.JobRegulationId == id);
            ViewBag.JobId = id;
            return View(jr);
        }

        [HttpPost]
        public IActionResult RemoveRegulation(JobRegulation jobRegulation)
        {
            int jobId = jobRegulation.JobId;
            context.JobRegulations.Remove(jobRegulation);
            context.SaveChanges();
            TempData["successMessage"] = "Regulation Removed successfully";
            return RedirectToAction("View", new { id = jobId });
        }

        public IActionResult AddRegulation(int id)
        {
            var regulations = context.Regulations.Include(r => r.RegulationRequirements).ToList();
            ViewBag.JobId = id;
            return View(regulations);
        }

        public IActionResult AssignRegulation(int regulationId, int jobId)
        {
            JobRegulation jobRegulation = new JobRegulation
            {
                RegulationId = regulationId,
                JobId = jobId
            };
            context.JobRegulations.Add(jobRegulation);
            context.SaveChanges();
            return RedirectToAction("View", new { id = jobId});
        }
    }
}
