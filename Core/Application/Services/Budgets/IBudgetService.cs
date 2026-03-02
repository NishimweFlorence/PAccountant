using System.Collections.Generic;
using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBudgetService
    {
        List<Budget> GetAllBudgets();
        Budget GetBudgetById(int id);
        void CreateBudget(BudgetCreateDTO budgetCreateDTO);
        void UpdateBudget(int id, BudgetUpdateDTO budgetUpdateDTO);
        void DeleteBudget(int id);
        List<BudgetDisplayDTO> GetBudgetsWithUsage(int month, int year);
    }
}
