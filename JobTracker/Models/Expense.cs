using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JobTracker.Models
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
        public decimal Amount { get; set; }
        [Column(TypeName = "image")]
        public byte[] Receipt { get; set; }
        public string Description { get; set; }
    }
}
