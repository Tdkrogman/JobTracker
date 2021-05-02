using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobTracker.Models;

namespace JobTracker.ViewModels
{
    public class AssignTaskViewModel
    {
        public TaskAssignment TaskAssignment { get; set; }
        public Models.Task Task { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Contractor> Contractors { get; set; }
    }
}
