using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBudget
    {
        List<Budget> GetAllBudgets();
        Budget GetBudgetById(int id);
        void CreateBudget(BudgetCreateDTO budgetCreateDTO);
        void UpdateBudget(int id, BudgetUpdateDTO budgetUpdateDTO);
        void DeleteBudget(int id);
        
        // Business Logic
        List<BudgetDisplayDTO> GetBudgetsWithUsage(int month, int year);
    }
}
