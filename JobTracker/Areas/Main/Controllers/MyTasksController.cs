using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using JobTracker.ViewModels;

namespace JobTracker.Controllers
{
    public class MyTasksController : Controller
    {
        private readonly JobTrackerContext context;

        public MyTasksController(JobTrackerContext ctx)
        {
            context = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        public IActionResult List()
        {
            var vm = new EmployeeTaskViewModel
            {
                TaskAssignments = context.TaskAssignments.Where(ta => ta.EmployeeId == HttpContext.Session.GetInt32("currentUserId")).Include(t => t.Task).Include(ta => ta.Task.Job).ToList()
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult UpdateTask(Models.Task task)
        {
            string jobName = task.Job.Name;
            task.Job = null;
            context.Tasks.Update(task);
            context.SaveChanges();
            TempData["successMessage"] = jobName + ": " + task.Name + " successfully updated.";
            return RedirectToAction("List");
        }       
    }
}
