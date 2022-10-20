using ATM.Core.Validation;

namespace ATM.Core.Interfaces.Validation
{
    public interface IOperationValidator
    {
        OperationResult ValidateUserAccount(string userID, string accountNumber);

        OperationResult ValidateAmount(decimal amount);

        OperationResult ValidateAmountForPayment(decimal balance, decimal amountToSubtract, bool validateMultiple20 = true);
    }
}