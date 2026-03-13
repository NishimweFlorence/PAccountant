using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services.Budgets
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudget _budgetRepo;
        private readonly ITransactionService _transactionService;

        public BudgetService(
            IBudget budgetRepo, 
            ITransactionService transactionService)
        {
            _budgetRepo = budgetRepo;
            _transactionService = transactionService;
        }

        public async Task<List<Budget>> GetAllBudgetsAsync()
        {
            return await _budgetRepo.GetAllBudgetsAsync();
        }

        public async Task<Budget?> GetBudgetByIdAsync(int id)
        {
            return await _budgetRepo.GetBudgetByIdAsync(id);
        }

        public async Task CreateBudgetAsync(BudgetCreateDTO budgetCreateDTO)
        {
            await _budgetRepo.CreateBudgetAsync(budgetCreateDTO);
        }

        public async Task UpdateBudgetAsync(int id, BudgetUpdateDTO budgetUpdateDTO)
        {
            await _budgetRepo.UpdateBudgetAsync(id, budgetUpdateDTO);
        }

        public async Task DeleteBudgetAsync(int id)
        {
            await _budgetRepo.DeleteBudgetAsync(id);
        }

        public async Task<List<BudgetDisplayDTO>> GetBudgetsWithUsageAsync(DateTime targetDate)
        {
            var budgets = await _budgetRepo.GetAllBudgetsAsync();
            var allTransactions = await _transactionService.GetAllTransactionsAsync();
            
            var result = new List<BudgetDisplayDTO>();

            // Filter budgets active for the target month/year
            var startOfMonth = new DateTime(targetDate.Year, targetDate.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            var activeBudgets = budgets.Where(b => 
                (b.StartDate >= startOfMonth && b.StartDate <= endOfMonth) ||
                (b.EndDate >= startOfMonth && b.EndDate <= endOfMonth) ||
                (b.StartDate <= startOfMonth && b.EndDate >= endOfMonth));

            foreach (var budget in activeBudgets)
            {
                decimal spent = 0;
                
                if (budget.TransactionCategoryId.HasValue)
                {
                    // Calculate spent based on category and date range
                    var categoryTransactions = allTransactions.Where(t => 
                        t.TransactionDate >= budget.StartDate && 
                        t.TransactionDate <= budget.EndDate);

                    foreach (var t in categoryTransactions)
                    {
                        spent += t.Entries
                            .Where(e => e.CategoryId == budget.TransactionCategoryId.Value)
                            .Sum(e => e.Debit);
                    }
                }
                else
                {
                    // Fallback to explicit budget link
                    var budgetTransactions = allTransactions.Where(t => t.BudgetId == budget.Id);
                    foreach (var t in budgetTransactions)
                    {
                        spent += t.Entries.Where(e => e.CategoryId.HasValue).Sum(e => e.Debit);
                    }
                }

                result.Add(new BudgetDisplayDTO
                {
                    Id = budget.Id,
                    Name = budget.Name,
                    Amount = budget.Amount,
                    SpentAmount = spent,
                    StartDate = budget.StartDate,
                    EndDate = budget.EndDate,
                    CategoryId = budget.TransactionCategoryId,
                    CategoryName = budget.TransactionCategory?.Name
                });
            }

            return result;
        }
    }
}
