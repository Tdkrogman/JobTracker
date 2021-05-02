using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobTracker.Models;

namespace JobTracker.ViewModels
{
    public class JobViewModel
    {
        public Job Job { get; set; }
        public IReadOnlyCollection<Models.Task> Tasks { get; set; }
        public IReadOnlyCollection<Employee> Employees { get; set; }
        public IReadOnlyCollection<Contractor> Contractors { get; set; }
        public IReadOnlyCollection<JobRegulation> JobRegulations { get; set; }
    }
}
