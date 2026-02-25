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


        
    }
}