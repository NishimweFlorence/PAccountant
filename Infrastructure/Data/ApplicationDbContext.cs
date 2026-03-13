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
        public DbSet<TransactionEntry> TransactionEntries { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<CustomLookup> CustomLookups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(entity =>
            {
                entity.OwnsOne(a => a.Currency);
                entity.OwnsOne(a => a.Type, t =>
                {
                    t.Property(x => x.Name).HasColumnName("Type");
                });
                entity.HasMany(a => a.TransactionEntries)
                      .WithOne(e => e.Account)
                      .HasForeignKey(e => e.AccountId);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.OwnsOne(t => t.Currency);
                entity.HasMany(t => t.Entries)
                      .WithOne(e => e.Transaction)
                      .HasForeignKey(e => e.TransactionId);

                entity.HasOne(t => t.Budget)
                      .WithMany(b => b.Transactions)
                      .HasForeignKey(t => t.BudgetId);
            });

            modelBuilder.Entity<Asset>(entity =>
            {
                entity.OwnsOne(a => a.Currency);
                entity.OwnsOne(a => a.Type);
                entity.OwnsOne(a => a.ValueChange);
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
                // Decoupled from Category
            });

            modelBuilder.Entity<TransactionCategory>(entity =>
            {
                entity.OwnsOne(c => c.Type);
                entity.HasMany(c => c.TransactionEntries)
                      .WithOne(e => e.Category)
                      .HasForeignKey(e => e.CategoryId);
            });

            modelBuilder.Entity<Income>(entity =>
            {
                entity.OwnsOne(i => i.Type);
                entity.OwnsOne(i => i.Currency);
                entity.HasOne(i => i.Transaction)
                      .WithMany()
                      .HasForeignKey(i => i.TransactionId);
            });

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.OwnsOne(e => e.Type);
                entity.OwnsOne(e => e.Currency);
                entity.HasOne(e => e.Transaction)
                      .WithMany()
                      .HasForeignKey(e => e.TransactionId);
            });

            modelBuilder.Entity<CustomLookup>(entity =>
            {
                entity.HasOne(c => c.User)
                      .WithMany()
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
