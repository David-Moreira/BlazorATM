using System.ComponentModel.DataAnnotations.Schema;

namespace ATM.Core.Entities
{
    public class Transaction
    {
        public int ID { get; set; }

        public string Amount { get; set; }

        public string Recipient { get; set; }

        [ForeignKey("BankAccount")]
        public string AccountNumber { get; set; }

        public virtual BankAccount BankAccount { get; set; }
    }
}