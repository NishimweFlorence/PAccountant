using System.Collections.Generic;
using System.Linq;
using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BudgetRepository : IBudget
    {
        private readonly ApplicationDbContext _context;

        public BudgetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Budget>> GetAllBudgetsAsync()
        {
            return await _context.Budgets.ToListAsync();
        }

        public async Task<Budget?> GetBudgetByIdAsync(int id)
        {
            return await _context.Budgets.FindAsync(id);
        }

        public async Task CreateBudgetAsync(BudgetCreateDTO budgetCreateDTO)
        {
            var budget = new Budget
            {
                Name       = budgetCreateDTO.Name,
                Amount     = budgetCreateDTO.Amount,
                StartDate  = budgetCreateDTO.StartDate,
                EndDate    = budgetCreateDTO.EndDate,
                TransactionCategoryId = budgetCreateDTO.CategoryId,
                CreatedAt  = System.DateTime.Now
            };
            _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBudgetAsync(int id, BudgetUpdateDTO budgetUpdateDTO)
        {
            var budget = await _context.Budgets.FindAsync(id);
            if (budget != null)
            {
                budget.Name      = budgetUpdateDTO.Name;
                budget.Amount    = budgetUpdateDTO.Amount;
                budget.StartDate = budgetUpdateDTO.StartDate;
                budget.EndDate   = budgetUpdateDTO.EndDate;
                budget.TransactionCategoryId = budgetUpdateDTO.CategoryId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteBudgetAsync(int id)
        {
            var budget = await _context.Budgets.FindAsync(id);
            if (budget != null)
            {
                _context.Budgets.Remove(budget);
                await _context.SaveChangesAsync();
            }
        }
    }
}
