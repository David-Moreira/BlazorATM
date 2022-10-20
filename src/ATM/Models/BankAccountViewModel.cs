using System;
using System.ComponentModel.DataAnnotations;

namespace ATM.Models
{
    public class BankAccountViewModel
    {
        [Required]
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}