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
        public DbSet<Account> Accounts { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }
        
        public DbSet<Asset> Assets { get; set; }

        
    }
}