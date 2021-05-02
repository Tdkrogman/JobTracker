using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobTracker.Models
{
    public class TaskAssignment
    {
        public int TaskAssignmentId { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }
    }
}
