using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobTracker.Models;

namespace JobTracker.ViewModels
{
    public class RegulationListViewModel
    {
        public Regulation Regulation { get; set; }

        public IEnumerable<RegulationRequirement> RegulationRequirements { get; set; }

    }
}
