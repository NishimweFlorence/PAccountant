using System.Collections.Generic;
using System.Linq;
using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class BudgetRepository : IBudget
    {
        private readonly ApplicationDbContext _context;

        public BudgetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Budget> GetAllBudgets()
        {
            return _context.Budgets.ToList();
        }

        public Budget GetBudgetById(int id)
        {
            return _context.Budgets.Find(id);
        }

        public void CreateBudget(BudgetCreateDTO budgetCreateDTO)
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
            _context.SaveChanges();
        }

        public void UpdateBudget(int id, BudgetUpdateDTO budgetUpdateDTO)
        {
            var budget = _context.Budgets.Find(id);
            if (budget != null)
            {
                budget.Amount = budgetUpdateDTO.Amount;
                budget.Month  = budgetUpdateDTO.Month;
                budget.Year   = budgetUpdateDTO.Year;
                _context.SaveChanges();
            }
        }

        public void DeleteBudget(int id)
        {
            var budget = _context.Budgets.Find(id);
            if (budget != null)
            {
                _context.Budgets.Remove(budget);
                _context.SaveChanges();
            }
        }

        public List<BudgetDisplayDTO> GetBudgetsWithUsage(int month, int year)
        {
            // This is handled in the Service layer to combine Repository data with Transactions
            throw new System.NotImplementedException("Usage calculation is handled in BudgetService");
        }
    }
}
