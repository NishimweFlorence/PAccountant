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
        public DbSet<Asset> Assets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Budget> Budgets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(entity =>
            {
                entity.OwnsOne(a => a.Currency);
                entity.HasMany(a => a.Transactions)
                      .WithOne(t => t.Account)
                      .HasForeignKey(t => t.AccountId);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.OwnsOne(t => t.Currency);
                entity.HasOne(t => t.Category)
                      .WithMany(c => c.Transactions)
                      .HasForeignKey(t => t.IdTransactionCategory);
            });

            modelBuilder.Entity<Asset>(entity =>
            {
                entity.OwnsOne(a => a.Currency);
            });

            modelBuilder.Entity<Liability>(entity =>
            {
                entity.OwnsOne(l => l.Currency);
            });

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.OwnsOne(l => l.Currency);
            });

            modelBuilder.Entity<Budget>(entity =>
            {
                entity.HasOne(b => b.Category)
                      .WithMany(c => c.Budgets)
                      .HasForeignKey(b => b.CategoryId);
            });

            modelBuilder.Entity<TransactionCategory>(entity =>
            {
                entity.OwnsOne(c => c.Type);
            });
        }
    }
}
