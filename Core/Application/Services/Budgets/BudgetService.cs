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

        public List<Budget> GetAllBudgets()
        {
            return _budgetRepo.GetAllBudgets();
        }

        public Budget GetBudgetById(int id)
        {
            return _budgetRepo.GetBudgetById(id);
        }

        public void CreateBudget(BudgetCreateDTO budgetCreateDTO)
        {
            _budgetRepo.CreateBudget(budgetCreateDTO);
        }

        public void UpdateBudget(int id, BudgetUpdateDTO budgetUpdateDTO)
        {
            _budgetRepo.UpdateBudget(id, budgetUpdateDTO);
        }

        public void DeleteBudget(int id)
        {
            _budgetRepo.DeleteBudget(id);
        }

        public List<BudgetDisplayDTO> GetBudgetsWithUsage(int month, int year)
        {
            var budgets = _budgetRepo.GetAllBudgets();
            var categories = _categoryService.GetAllTransactionCategories();
            var transactions = _transactionService.GetAllTransactions()
                .Where(t => t.TransactionDate.Month == month && t.TransactionDate.Year == year)
                .ToList();

            var result = new List<BudgetDisplayDTO>();

            foreach (var budget in budgets.Where(b => b.Month == month && b.Year == year))
            {
                var category = _categoryService.GetTransactionCategoryById(budget.CategoryId);
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
