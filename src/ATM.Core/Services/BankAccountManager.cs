using ATM.Core.Entities;
using ATM.Core.Interfaces;
using ATM.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATM.Core.Services
{
    public class BankAccountManager : ServiceBase<BankAccount>, IBankAccountService
    {
        private readonly IBankAccountRepo _bankAccountRepo;

        public BankAccountManager(IBankAccountRepo bankAccountRepo) : base(bankAccountRepo)
        {
            _bankAccountRepo = bankAccountRepo;
        }

        public override BankAccount Add(BankAccount newAcc)
        {
            if (String.IsNullOrEmpty(newAcc.UserID))
                throw new Exception("No user associated with account.");

            var accountExists = GetAll().LastOrDefault();
            if (accountExists != null)
                newAcc.AccountNumber = (int.Parse(accountExists.AccountNumber) + 1).ToString().PadLeft(15, '0');
            else
                newAcc.AccountNumber = ("1").PadLeft(15, '0');
            return base.Add(newAcc);
        }

        public decimal GetAccountBalance(string id)
        {
            return GetByAccountNumber(id).Balance;
        }

        public BankAccount GetByAccountNumber(string id)
        {
            return _bankAccountRepo.GetSingle(x => x.AccountNumber == id);
        }

        public BankAccount GetByUserId(string id)
        {
            return _bankAccountRepo.GetSingle(x => x.UserID == id);
        }

        public IEnumerable<BankAccount> GetMultipleByUserId(string id)
        {
            return _bankAccountRepo.GetMultiple(x => x.UserID == id);
        }
    }
}