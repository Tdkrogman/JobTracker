using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobTracker.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        [Required]
        public int JobId { get; set; }
        public Job Job { get; set; }
        [Required(ErrorMessage ="Task requires a description.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Task requires a name.")]
        public string Name { get; set; }
        public string Status { get; set; } = "Not Started";
        [DataType(DataType.Date)]
        public DateTime EstCompletionDate { get; set; } = DateTime.Now;
        public ICollection<TaskAssignment> TaskAssignments { get; set; }        
    }
}
