using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobTracker.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "First name is required.")]
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
        [Remote("CheckCustomerEmail", "Validation", AdditionalFields = "CustomerId")]
        [StringLength(75, ErrorMessage = "Must be fewer than 75 characters long.")]
        public string Email { get; set; }
        public string FullName => FName + " " + LName;
    }
}
