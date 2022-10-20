using ATM.Core.Entities;
using System.Collections.Generic;

namespace ATM.Core.Interfaces.Services
{
    public interface IBankAccountService : IServiceBase<BankAccount>
    {
        BankAccount GetByUserId(string id);

        IEnumerable<BankAccount> GetMultipleByUserId(string id);

        BankAccount GetByAccountNumber(string id);

        decimal GetAccountBalance(string id);
    }
}