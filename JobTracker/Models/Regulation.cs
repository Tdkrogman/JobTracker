using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobTracker.Models
{
    public class Regulation
    {
        public int RegulationId { get; set; }
        [Required(ErrorMessage ="Regulation requires a name.")]

        public string Name { get; set; }
        [Required(ErrorMessage ="Regulation requires a description.")]
        public string Description { get; set; }
        public ICollection<RegulationRequirement> RegulationRequirements { get; set; }
    }
}
