using System.ComponentModel.DataAnnotations;

namespace ATM.Models
{
    public class AvailableAccountsViewModel
    {
        [Display(Name = "Account Number:")]
        public string AccountNumber { get; set; }

        [Display(Name = "Account Name:")]
        public string AccountName { get; set; }

        [Display(Name = "Account Balance:")]
        public decimal AccountBalance { get; set; }

        [Display(Name = "Account Holder:")]
        public string AccountHolder { get; set; }
    }
}