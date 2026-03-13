using Application.DTO;
using Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Incomes
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncome _incomeRepository;

        public IncomeService(IIncome incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public async Task<IncomeReadDTO> CreateIncomeAsync(IncomeCreateDTO incomeDto)
        {
            return await _incomeRepository.CreateIncomeAsync(incomeDto);
        }

        public async Task<List<IncomeReadDTO>> GetAllIncomesAsync()
        {
            return await _incomeRepository.GetAllIncomesAsync();
        }

        public async Task<IncomeReadDTO?> GetIncomeByIdAsync(int id)
        {
            return await _incomeRepository.GetIncomeByIdAsync(id);
        }
    }
}
