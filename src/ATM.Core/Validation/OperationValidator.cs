using ATM.Core.Interfaces.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Core.Validation
{
    public class OperationValidator : IOperationValidator
    {
        public OperationResult ValidateAmount(decimal amount)
        {
            var errors = new List<OperationError>();
            if (amount < 0)
            {
                errors.Add(new OperationError(OperationError.Error.ValueCannotBeNegative));
            }
            return errors.Count == 0 ? OperationResult.Success() : OperationResult.Failed(errors.ToArray());
        }

        public OperationResult ValidateAmountForPayment(decimal balance, decimal amountToSubtract)
        {
            var errors = new List<OperationError>();
            var result = ValidateAmount(balance);

            if (balance - amountToSubtract < 0)
            {
                errors.Add(new OperationError(OperationError.Error.InsufficientFunds));
            }

            result.AddErrors(errors.ToArray());
            return result;
        }

        public OperationResult ValidateUserAccount(string userID, string accountNumber)
        {
            return OperationResult.Success();
        }
    }
}