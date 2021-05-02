using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobTracker.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage ="First name is required.")]
        [StringLength(50, ErrorMessage = "Must be fewer than 50 characters long.")]
        public string FName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Must be fewer than 50 characters long.")]
        public string LName { get; set; }
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression("((\\(\\d{3}\\) ?)|(\\d{3}-))?\\d{3}-\\d{4}", ErrorMessage = "Phone number must use xxx-xxx-xxxx or (xxx) xxx-xxxx format.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        [Remote("CheckEmployeeEmail", "Validation", AdditionalFields = "EmployeeId")]
        [StringLength(75, ErrorMessage = "Must be fewer than 75 characters long.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        [StringRange("Username", 4, 20)]
        [Remote("CheckUsername", "Validation", AdditionalFields = "EmployeeId")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [StringRange("Password", 4, 25)]
        public string Password { get; set; }
        public string FullName => FName + " " + LName;
    }
}
