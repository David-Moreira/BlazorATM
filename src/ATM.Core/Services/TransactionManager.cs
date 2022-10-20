using ATM.Core.Entities;
using ATM.Core.Interfaces;
using ATM.Core.Interfaces.Services;

namespace ATM.Core.Services
{
    public class TransactionManager : ServiceBase<Transaction>, ITransactionService
    {
        private readonly ITransactionRepo _transactionRepo;

        public TransactionManager(ITransactionRepo transactionRepo) : base(transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }
    }
}