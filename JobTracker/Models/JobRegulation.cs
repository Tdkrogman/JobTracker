using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobTracker.Models
{
    public class JobRegulation
    {
        public int JobRegulationId { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
        public int RegulationId { get; set; }
        public Regulation Regulation { get; set; }
    }
}
