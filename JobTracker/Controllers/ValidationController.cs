using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobTracker.Models;
using System.ComponentModel.DataAnnotations;

namespace JobTracker.Controllers
{
    public class ValidationController : Controller
    {
        private readonly JobTrackerContext context;

        public ValidationController(JobTrackerContext ctx)
        {
            context = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        public JsonResult CheckEmployeeEmail(string Email, int EmployeeId)
        {
            var empsWithMatchingEmails = context.Employees.Where(e => e.Email == Email);

            if (!(new EmailAddressAttribute().IsValid(Email)))
                return Json($"Invalid Email address.");

            foreach (var employee in empsWithMatchingEmails)
            {
                if (employee.Email == Email && employee.EmployeeId != EmployeeId)
                    return Json($"Email address already in use.");
            }
            return Json(true);
        }

        public JsonResult CheckContractorEmail(string Email, int ContractorId)
        {
            var consWithMatchingEmails = context.Contractors.Where(e => e.Email == Email);

            if (!(new EmailAddressAttribute().IsValid(Email)))
                return Json($"Invalid Email address.");

            foreach (var contractor in consWithMatchingEmails)
            {
                if (contractor.Email == Email && contractor.ContractorId != ContractorId)
                    return Json($"Email address already in use.");
            }
            return Json(true);
        }

        public JsonResult CheckCustomerEmail(string Email, int CustomerId)
        {
            var custWithMatchingEmails = context.Customers.Where(e => e.Email == Email);

            if (!(new EmailAddressAttribute().IsValid(Email)))
                return Json($"Invalid Email address.");

            foreach (var customer in custWithMatchingEmails)
            {
                if (customer.Email == Email && customer.CustomerId != CustomerId)
                    return Json($"Email address already in use.");
            }
            return Json(true);
        }

        public JsonResult CheckUsername(string Username, int EmployeeId)
        {
            var empsWithMatchingUsernames = context.Employees.Where(e => e.Username == Username);

            foreach (var employee in empsWithMatchingUsernames)
            {
                if (employee.Username.ToLower().Equals(Username.ToLower()) && employee.EmployeeId != EmployeeId)
                    return Json($"Username already in use.");
            }
            return Json(true);
        }
    }
}
