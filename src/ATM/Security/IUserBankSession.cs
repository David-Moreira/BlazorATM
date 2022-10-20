using ATM.Core.Entities;
using ATM.Core.Facade;
using ATM.Core.Interfaces.Services;
using ATM.Core.Validation;
using ATM.Models;
using ATM.Pages.Operations;

namespace ATM.Security
{
    public class UserBankSession : IUserBankSession
    {
        private readonly IUserSession _userSession;
        private readonly IBankAccountService _bankAccountService;
        private readonly IOperationService _operationService;
        private string _currentAccountNumber;

        public event EventHandler<string> OnCurrentAccountChanged;

        public UserBankSession(IUserSession userSession, IBankAccountService bankAccountService, IOperationService operationService)
        {
            this._userSession = userSession;
            this._bankAccountService = bankAccountService;
            this._operationService = operationService;
        }
        private async Task<BankAccount> GetAccount()
            => _bankAccountService.GetByUserId(((await _userSession.GetLoggedInUser()).Email));

        public async Task<BankAccount> GetCurrentAccount()
            => _bankAccountService.GetByAccountNumber(await GetCurrentAccountNumber());

        public async Task<decimal> GetCurrentAccountBalance()
         => _bankAccountService.GetAccountBalance(await GetCurrentAccountNumber());

        public async Task<string> GetCurrentAccountNumber()
        {
            if (string.IsNullOrEmpty(_currentAccountNumber))
                _currentAccountNumber = (await GetAccount()).AccountNumber;

            return _currentAccountNumber;
        }

        public Task SelectAccount(string number)
        {
            var account = _bankAccountService.GetByAccountNumber(number);
            if (account is not null) { 
                _currentAccountNumber = account.AccountNumber;
                OnCurrentAccountChanged.Invoke(this, _currentAccountNumber);
            }

            return Task.CompletedTask;
        }

        public async Task<OperationResult> QuickCash()
        {
            return _operationService.QuickCash(await GetCurrentAccountNumber());
        }

        public async Task<OperationResult> Deposit(decimal amount)
        {
            return _operationService.Deposit(await GetCurrentAccountNumber(), amount);
        }

        public async Task<OperationResult> Withdraw(decimal amount)
        {
            return _operationService.Withdraw(await GetCurrentAccountNumber(), amount);
        }


        public async Task<List<AvailableAccountsViewModel>> GetAvailableAccounts()
        {
            List<AvailableAccountsViewModel> availableAccounts = new List<AvailableAccountsViewModel>();
            IEnumerable<BankAccount> bankAccounts = _bankAccountService.GetMultipleByUserId((await _userSession.GetLoggedInUser()).Email);
            foreach (var account in bankAccounts)
            {
                availableAccounts.Add(
                    new AvailableAccountsViewModel()
                    {
                        AccountNumber = account.AccountNumber,
                        AccountName = account.AccountName,
                        AccountBalance = account.Balance,
                        AccountHolder = account.AccountHolder
                    }
                );
            }
            return availableAccounts;
        }

        public async Task<OperationResult> CreateBankAccount(BankAccountViewModel model)
        {
            _bankAccountService.Add(new BankAccount() { AccountName = model.AccountName, UserID = (await _userSession.GetLoggedInUser()).Email, FirstName = model.FirstName, LastName = model.LastName });
            return OperationResult.Success();
        }

        public Task<OperationResult> TransferFunds(TransferFundsViewModel model)
            => Task.FromResult(_operationService.TransferFunds(model.accountNumber, model.recipientAccountNumber, model.Amount));
    }
    public interface IUserBankSession
    {
        public event EventHandler<string> OnCurrentAccountChanged;

        Task<OperationResult> TransferFunds(TransferFundsViewModel transferFunds);

        Task<OperationResult> CreateBankAccount(BankAccountViewModel bankAccount);

        Task<List<AvailableAccountsViewModel>> GetAvailableAccounts();

        Task<OperationResult> Withdraw(decimal amount);

        Task<OperationResult> Deposit(decimal amount);

        Task<decimal> GetCurrentAccountBalance();
        Task<BankAccount> GetCurrentAccount();
        Task<string> GetCurrentAccountNumber();
        Task SelectAccount(string number);
        Task<OperationResult> QuickCash();
    }
}
