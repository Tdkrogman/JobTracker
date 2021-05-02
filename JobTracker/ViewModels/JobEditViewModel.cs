using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobTracker.Models;

namespace JobTracker.ViewModels
{
    public class JobEditViewModel
    {
        public Job Job { get; set; }
        public List<Customer> Customers { get; set; }
        public string Action { get; set; }
    }
}
