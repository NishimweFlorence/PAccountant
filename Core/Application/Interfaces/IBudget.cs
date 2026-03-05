using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBudget
    {
        Task<List<Budget>> GetAllBudgetsAsync();
        Task<Budget?> GetBudgetByIdAsync(int id);
        Task CreateBudgetAsync(BudgetCreateDTO budgetCreateDTO);
        Task UpdateBudgetAsync(int id, BudgetUpdateDTO budgetUpdateDTO);
        Task DeleteBudgetAsync(int id);
    }
}
