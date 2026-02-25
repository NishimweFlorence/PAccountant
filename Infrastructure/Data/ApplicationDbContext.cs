using Domain.Entities;
using Application.DTO;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Liability> Liabilities { get; set; }
        public DbSet<Transaction> Transactions { get; set; }


        public DbSet<Account> Accounts { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanRepayment> LoanRepayments { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }
        
    }
}