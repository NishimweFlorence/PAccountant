using Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IExpenseService
    {
        Task<ExpenseReadDTO> CreateExpenseAsync(ExpenseCreateDTO expenseDto);
        Task<List<ExpenseReadDTO>> GetAllExpensesAsync();
        Task<ExpenseReadDTO?> GetExpenseByIdAsync(int id);
    }
}
