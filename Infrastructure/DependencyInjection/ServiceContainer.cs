using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Application.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;
using Domain.Entities;

namespace Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CRMDemoSQLConnection")), ServiceLifetime.Scoped
            );

            services.AddScoped<ILiability, LiabilityRepository>();
            services.AddScoped<ITransaction, TransactionsRepository>();
            services.AddScoped<IAccount, AccountRepository>();
            services.AddScoped<ILoan, LoanRepository>();
            services.AddScoped<ILoanRepayment, LoanRepayRepository>();
            services.AddScoped<ITransactionCategory, TransactionCategoryRepository>();
            services.AddScoped<IAsset, AssetRepository>();
        }
    }
}
