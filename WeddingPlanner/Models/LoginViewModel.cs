using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public class LoginViewModel
    {        
        [Display(Name = "Email:")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Password:")]
        [Required]
        [DataType (DataType.Password)]
        [MinLength (8, ErrorMessage="Must have more than 8 characters!")]
        public string Password { get; set; }
    }
}