using Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IIncome
    {
        Task<IncomeReadDTO> CreateIncomeAsync(IncomeCreateDTO incomeDto);
        Task<List<IncomeReadDTO>> GetAllIncomesAsync();
        Task<IncomeReadDTO?> GetIncomeByIdAsync(int id);
    }
}
