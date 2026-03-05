using System;
using System.Collections.Generic;
using System.Linq;
using Application.DTO;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services.Budgets
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudget _budgetRepo;
        private readonly ITransactionService _transactionService;
        private readonly ITransactionCategoryService _categoryService;

        public BudgetService(
            IBudget budgetRepo, 
            ITransactionService transactionService,
            ITransactionCategoryService categoryService)
        {
            _budgetRepo = budgetRepo;
            _transactionService = transactionService;
            _categoryService = categoryService;
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

        public async Task<List<BudgetDisplayDTO>> GetBudgetsWithUsageAsync(int month, int year)
        {
            var budgets = await _budgetRepo.GetAllBudgetsAsync();
            var categories = await _categoryService.GetAllTransactionCategoriesAsync(); // This line was in the user's snippet, but not used directly. Keeping it as per instruction.
            var allTransactions = await _transactionService.GetAllTransactionsAsync();
            
            var transactions = allTransactions
                .Where(t => t.TransactionDate.Month == month && t.TransactionDate.Year == year)
                .ToList();

            var result = new List<BudgetDisplayDTO>();

            foreach (var budget in budgets.Where(b => b.Month == month && b.Year == year))
            {
                var category = await _categoryService.GetTransactionCategoryByIdAsync(budget.CategoryId);
                var spent = transactions
                    .Where(t => t.IdTransactionCategory == budget.CategoryId)
                    .Sum(t => t.Amount);

                result.Add(new BudgetDisplayDTO
                {
                    Id = budget.Id,
                    CategoryId = budget.CategoryId,
                    CategoryName = category?.Name ?? "Unknown",
                    Amount = budget.Amount,
                    SpentAmount = spent,
                    Month = budget.Month,
                    Year = budget.Year
                });
            }

            return result;
        }
    }
}
