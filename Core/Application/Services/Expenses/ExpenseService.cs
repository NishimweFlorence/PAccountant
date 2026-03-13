using Application.DTO;
using Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Expenses
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpense _expenseRepository;

        public ExpenseService(IExpense expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<ExpenseReadDTO> CreateExpenseAsync(ExpenseCreateDTO expenseDto)
        {
            return await _expenseRepository.CreateExpenseAsync(expenseDto);
        }

        public async Task<List<ExpenseReadDTO>> GetAllExpensesAsync()
        {
            return await _expenseRepository.GetAllExpensesAsync();
        }

        public async Task<ExpenseReadDTO?> GetExpenseByIdAsync(int id)
        {
            return await _expenseRepository.GetExpenseByIdAsync(id);
        }
    }
}
