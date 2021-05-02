using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobTracker.Models
{
    public class WorkHour
    {
        public int WorkHourId { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;
        [Range(0.25, 18, ErrorMessage ="Number of hours must be between 1 and 18.")]
        public double Hours { get; set; }
    }
}