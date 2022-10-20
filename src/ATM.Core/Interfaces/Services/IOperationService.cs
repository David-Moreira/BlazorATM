using ATM.Core.Validation;

namespace ATM.Core.Interfaces.Services
{
    public interface IOperationService
    {
        OperationResult Withdraw(string accountNumber, decimal amount);

        OperationResult Deposit(string accountNumber, decimal amount);

        OperationResult Payment(string accountNumber, string recipientAccountNumber, decimal amount);

        OperationResult TransferFunds(string accountNumber, string recipientAccountNumber, decimal amount);

        OperationResult QuickCash(string accountNumber);

        string PrintStatement(string accountNumber);
    }
}