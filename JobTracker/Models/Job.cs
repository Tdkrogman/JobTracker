using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JobTracker.Models
{
    public class Job
    {
        public int JobId { get; set; }
        [Required(ErrorMessage = "Customer is required.")]
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now;
        public string Status { get; set; }
        [Column(TypeName = "image")]
        public byte[] Estimate { get; set; }
    }
}
