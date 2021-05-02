using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobTracker.Models;

namespace JobTracker.ViewModels
{
    public class JobListViewModel
    {
        public IEnumerable<Job> Jobs { get; set; }
        public string Filter { get; set; }
    }
}
