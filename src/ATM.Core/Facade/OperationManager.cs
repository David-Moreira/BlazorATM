using ATM.Core.Entities;
using ATM.Core.Interfaces.Services;
using ATM.Core.Interfaces.Validation;
using ATM.Core.Validation;
using System;

namespace ATM.Core.Facade
{
    public class OperationManager : IOperationService
    {
        private readonly IBankAccountService _bankManager;
        private readonly ITransactionService _transactionManager;
        private readonly IOperationValidator _operationValidator;

        private BankAccount _bankAccount;
        private Transaction _transaction;

        public OperationManager(IBankAccountService bankManager, ITransactionService transactionManager, IOperationValidator operationValidator)
        {
            _operationValidator = operationValidator;
            _bankManager = bankManager;
            _transactionManager = transactionManager;
            _transaction = new Transaction();
        }

        //Have to rethink this with transactions
        public OperationResult Deposit(string accountNumber, decimal amount)
        {
            var result = _operationValidator.ValidateAmount(amount);
            if (!result.Succeeded)
                return result;

            _bankAccount = _bankManager.GetByAccountNumber(accountNumber);
            _bankAccount.Balance += amount;
            _transaction.AccountNumber = accountNumber;

            _transaction.Amount = $"+ {amount}";
            _transaction.Recipient = _bankAccount.AccountNumber;
            _bankManager.Update(_bankAccount);
            _transactionManager.Add(_transaction);

            return result;
        }

        public OperationResult Payment(string accountNumber, string recipientAccountNumber, decimal amount)
        {
            _bankAccount = _bankManager.GetByAccountNumber(accountNumber);
            var recipientAcc = _bankManager.GetByAccountNumber(recipientAccountNumber);

            var result = _operationValidator.ValidateAmountForPayment(_bankAccount.Balance, amount);
            if (!result.Succeeded)
                return result;

            _bankAccount.Balance -= amount;
            _transaction.AccountNumber = accountNumber;

            _transaction.Amount = "-" + amount.ToString();
            _transaction.Recipient = _bankAccount.AccountNumber;

            recipientAcc.Balance += amount;

            Transaction recipientTransaction = new Transaction();
            recipientTransaction.AccountNumber = _bankAccount.AccountNumber;
            recipientTransaction.Amount = "+" + amount.ToString();
            recipientTransaction.Recipient = recipientAcc.AccountNumber;

            _bankManager.Update(recipientAcc);
            _bankManager.Update(_bankAccount);

            _transactionManager.Add(_transaction);
            _transactionManager.Add(recipientTransaction);

            return result;
        }

        public string PrintStatement(string accountNumber)
        {
            _bankAccount = _bankManager.GetByAccountNumber(accountNumber);
            return string.Format("Conta: {0} \nFull Name: {1}\nBalance: {2}", _bankAccount.AccountNumber, _bankAccount.AccountHolder, _bankAccount.Balance);
        }

        public OperationResult QuickCash(string accountNumber)
        {
            _bankAccount = _bankManager.GetByAccountNumber(accountNumber);

            var result = _operationValidator.ValidateAmountForPayment(_bankAccount.Balance, 10);
            if (!result.Succeeded)
                return result;

            _bankAccount.Balance -= 10;
            _transaction.AccountNumber = accountNumber;

            _transaction.Amount = "-10";
            _transaction.Recipient = _bankAccount.AccountNumber;

            _bankManager.Update(_bankAccount);
            _transactionManager.Add(_transaction);

            return result;
        }

        public OperationResult TransferFunds(string accountNumber, string recipientAccountNumber, decimal amount)
        {
            _bankAccount = _bankManager.GetByAccountNumber(accountNumber);
            var recipientAcc = _bankManager.GetByAccountNumber(recipientAccountNumber);

            var result = _operationValidator.ValidateAmountForPayment(_bankAccount.Balance, amount);
            if (!result.Succeeded)
                return result;

            _bankAccount.Balance -= amount;
            recipientAcc.Balance += amount;
            _transaction.AccountNumber = recipientAccountNumber;

            _transaction.Amount = "-" + amount.ToString();
            _transaction.Recipient = _bankAccount.AccountNumber;

            Transaction recipientTransaction = new Transaction();
            recipientTransaction.AccountNumber = _bankAccount.AccountNumber;
            recipientTransaction.Amount = "+" + amount.ToString();
            recipientTransaction.Recipient = recipientAcc.AccountNumber;

            _bankManager.Update(recipientAcc);
            _bankManager.Update(_bankAccount);

            _transactionManager.Add(_transaction);
            _transactionManager.Add(recipientTransaction);
            return result;
        }

        public OperationResult Withdraw(string accountNumber, decimal amount)
        {
            _bankAccount = _bankManager.GetByAccountNumber(accountNumber);

            var result = _operationValidator.ValidateAmountForPayment(_bankAccount.Balance, amount);
            if (!result.Succeeded)
                return result;

            _bankAccount.Balance -= amount;
            _transaction.AccountNumber = accountNumber;

            _transaction.Amount = "-" + amount.ToString();
            _transaction.Recipient = _bankAccount.AccountNumber;

            _bankManager.Update(_bankAccount);
            _transactionManager.Add(_transaction);
            return result;
        }
    }
}