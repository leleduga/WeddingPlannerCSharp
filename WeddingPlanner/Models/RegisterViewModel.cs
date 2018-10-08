using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public class RegisterViewModel
    {
        [Display(Name = "First Name:")]
        [Required]
        // [RegularExpression (@"^[a-zA-Z]+$"),ErrorMessage = "Name must only contain letters"]
        [MinLength (2, ErrorMessage="Must have more than 2 characters!")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name:")]
        [Required]
        // [RegularExpression (@"^[a-zA-Z]+$"),ErrorMessage = "Name must only contain letters"]
        [MinLength (2, ErrorMessage="Must have more than 2 characters!")]
        public string LastName { get; set; }

        [Display(Name = "Email:")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Password:")]
        [Required]
        [DataType (DataType.Password)]
        [MinLength (8, ErrorMessage="Must have more than 8 characters!")]
        public string Password { get; set; }

        [Display(Name = "ConfirmPassword:")]
        [Required]
        [Compare("Password", ErrorMessage = "Passwords must match!")]
        [DataType (DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}