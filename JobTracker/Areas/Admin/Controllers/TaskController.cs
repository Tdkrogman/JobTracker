using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobTracker.Models;
using Microsoft.AspNetCore.Http;

namespace JobTracker.Areas.Admin.Controllers
{
    public class TaskController : Controller
    {
        private readonly JobTrackerContext context;

        public TaskController(JobTrackerContext ctx)
        {
            context = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }
        [HttpGet]
        public IActionResult AddTask(int id)
        {
            Models.Task task = new Models.Task();
            task.JobId = id;
            ViewBag.Action = "Add";
            return View(task);
        }
        [HttpPost]
        public IActionResult AddTask(Models.Task task)
        {
            if (ModelState.IsValid)
            {
                if (task.TaskId == 0)
                    context.Tasks.Add(task);
                else
                    context.Tasks.Update(task);
                context.SaveChanges();
                TempData["successMessage"] = "Task added successfully.";
                return RedirectToAction("View", "Job", new { id = task.JobId });
            }
            else
            {

                return View(task);
            }
        }

        [HttpGet]
        public IActionResult EditTask(int id)
        {
            var task = context.Tasks.Find(id);
            ViewBag.Action = "Edit";
            return View("AddTask", task);
        }

        [HttpGet]
        public IActionResult DeleteTask(int id)
        {
            var t = context.Tasks.Find(id);
            return View(t);
        }

        [HttpPost]
        public IActionResult DeleteTask(Models.Task task)
        {
            context.Tasks.Remove(task);
            context.SaveChanges();
            TempData["successMessage"] = "Task deleted successfully.";
            return RedirectToAction("View", "Job", new { id = task.JobId });
        }

        [HttpGet]
        public IActionResult AssignTask(int id)
        {
            TaskAssignment ta = new TaskAssignment { TaskId = id };
            ViewBag.Task = context.Tasks.Find(id).Name;
            ViewBag.Employees = context.Employees.Skip(1).ToList();
            ViewBag.Contractors = context.Contractors.Skip(1).ToList();
            return View(ta);
        }
        [HttpPost]
        public IActionResult AssignTask(TaskAssignment ta)
        {
            if (ModelState.IsValid)
            {
                if (ta.EmployeeId != 0)
                    ta.ContractorId = -1;
                else
                    ta.EmployeeId = -1;

                context.TaskAssignments.Add(ta);
                context.SaveChanges();
                TempData["successMessage"] = "Task assigned successfully.";
            }
            else
            {
                TempData["errorMessage"] = "Task not assigned, please try again.";
                return View(ta);
            }
            return RedirectToAction("View", "Job", new { id = HttpContext.Session.GetInt32("currentJob") });
        }

        public IActionResult UnassignTask(int id)
        {
            TaskAssignment taskAssignment = context.TaskAssignments.Find(id);
            context.TaskAssignments.Remove(taskAssignment);
            context.SaveChanges();
            TempData["successMessage"] = "Task unassigned successfully.";
            return RedirectToAction("View", "Job", new { id = HttpContext.Session.GetInt32("currentJob") });
        }
    }
}
