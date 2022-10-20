using ATM.Core.Entities;
using ATM.Core.Interfaces;
using ATM.Infrastructure.Data;

namespace ATM.Infrastructure.Repositories
{
    public class BankAccountRepo : RepositoryBase<BankAccount>, IBankAccountRepo
    {
    }
}