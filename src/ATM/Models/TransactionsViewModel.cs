using System.ComponentModel.DataAnnotations;

namespace ATM.Models
{
    public abstract class StandardTransactionViewModel
    {
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C}")]
        public decimal Amount { get; set; }

        //[Required]
        [MinLength(length: 15, ErrorMessage = "Invalid Account, It should have 15 numbers.")]
        [MaxLength(length: 15, ErrorMessage = "Invalid Account, It should have 15 numbers.")]
        public string accountNumber { get; set; }
    }

    public class TransactionViewModel : StandardTransactionViewModel { } //Withdraw | Deposit

    public class TransferFundsViewModel : StandardTransactionViewModel //Transfer funds | Payment
    {
        [Required]
        [MinLength(length: 15, ErrorMessage = "Invalid Destinatary Account, It should have 15 numbers.")]
        [MaxLength(length: 15, ErrorMessage = "Invalid Destinatary Account, It should have 15 numbers.")]
        public string recipientAccountNumber { get; set; }
    }
}