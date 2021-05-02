using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobTracker.Models;

namespace JobTracker.ViewModels
{
    public class RegulationEditViewModel
    {
        public Regulation Regulation { get; set; }
        public IEnumerable<RegulationRequirement> RegulationRequirements { get; set; }
        public string Action { get; set; }
    }
}
