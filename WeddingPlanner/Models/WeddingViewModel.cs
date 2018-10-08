using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class ValidDateAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool result = false;
            if (value is DateTime)
            {
            if ((DateTime)value > DateTime.UtcNow)
            {
            result = true;
            }
            }
            return result;
        
        }
    }
}