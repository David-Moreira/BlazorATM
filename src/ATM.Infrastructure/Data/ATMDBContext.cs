using ATM.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace ATM.Infrastructure.Data
{
    public class ATMDBContext : DbContext
    {

        public ATMDBContext()
        {

        }

        public ATMDBContext(DbContextOptions<ATMDBContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }


        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}