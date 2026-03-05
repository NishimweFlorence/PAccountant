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
                CategoryId = budgetCreateDTO.CategoryId,
                Amount     = budgetCreateDTO.Amount,
                Month      = budgetCreateDTO.Month,
                Year       = budgetCreateDTO.Year,
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
                budget.Amount = budgetUpdateDTO.Amount;
                budget.Month  = budgetUpdateDTO.Month;
                budget.Year   = budgetUpdateDTO.Year;
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
