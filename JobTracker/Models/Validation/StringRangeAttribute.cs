using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace JobTracker.Models
{
    public class StringRangeAttribute : ValidationAttribute
    {
        public object checkVal;
        public int checkMax;
        public int checkMin;
        public StringRangeAttribute(object val, int min, int max)
        {
            checkVal = val;
            checkMax = max;
            checkMin = min;
        }

        protected override ValidationResult IsValid(object val, ValidationContext ctx)
        {
            if (!(val is string) || val.ToString().Length < checkMin || val.ToString().Length > checkMax)
            {
                return new ValidationResult(base.ErrorMessage ?? $"{ctx.DisplayName} must be greater than {checkMin - 1} and less than {checkMax + 1} characters long.");
            }
            return ValidationResult.Success;
           
        }
    }
}
