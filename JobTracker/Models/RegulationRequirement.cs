using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobTracker.Models
{
    public class RegulationRequirement
    {
        public int RegulationRequirementId { get; set; }
        public int RegulationId { get; set; }
        public Regulation Regulation { get; set; }
        [Required(ErrorMessage = "Must have a requirement.")]
        public string Requirement { get; set; }
    }
}
