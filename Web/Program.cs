using PAccountant.Components;
using MudBlazor.Services;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Application.Services.Liabilities;
using Application.Services.Transactions;
using Application.Services.Accounts;
using Application.Services.Loans;
using Application.Services.LoanRepayments;
using Infrastructure.DependencyInjection;
using Application.Services.Assets;
using Application.Services.TransactionCategories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

// Database connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CRMDemoSQLConnection")));

// Services implementation
builder.Services.AddScoped<ILiabilityService, LiabilityService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<ILoanRepaymentService, LoanRepaymentService>();
builder.Services.AddScoped<ITransactionCategoryService, TransactionCategoryService>();
builder.Services.AddScoped<IAssetService, AssetService>();

builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
