using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobTracker.Models;

namespace JobTracker.ViewModels
{
    public class EmployeeHoursViewModel
    {
        public Employee Employee { get; set; }
        public DateTime StartDate { get; set; }
        public IEnumerable<WorkHour> WorkHours { get; set;}
        public WorkHour WorkHour { get; set; }
        public int Id { get; set; }
    }
}
