using ATM.Core.Entities;
using ATM.Core.Interfaces;
using ATM.Infrastructure.Data;

namespace ATM.Infrastructure.Repositories
{
    public class TransactionRepo : RepositoryBase<Transaction>, ITransactionRepo
    {
        public TransactionRepo(ATMDBContext dbContext) : base(dbContext)
        {

        }
    }
}